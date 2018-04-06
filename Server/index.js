const DB_NAME = 'test_express_sqlite';
const Fs = require('fs');
const Path = require ('path');
const Sql = require ('sqlite3');
const Express = require('express');
const BP = require('body-parser');

const app = Express();

//app.use(BP.json());
app.use(BP.urlencoded({ extended: true }));


try {
    Fs.unlinkSync(`./${DB_NAME}`);
}
catch (_ign) {}


const DB = new Sql.Database(DB_NAME);

const init = Fs.readFileSync(Path.join(process.cwd(), 'init.sql'), 'utf-8');

DB.exec(init);


app.use((req, res, next) => {
    res.append('Access-Control-Allow-Origin', ['*']);
    res.append('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
    res.append('Access-Control-Allow-Headers', 'Content-Type');
    next();
});


/************ROUTES*************/

app.get('/:name', (req, res, next) => {
    DB.all('SELECT * FROM PLAYERS WHERE NAME = ?', [req.params.name], (err, data) => {
        if (err) {
            return next(err);
        }
        return res.json(data);
    });
});

app.post('/flats', (req, res, next) => {
    DB.run('INSERT INTO FLATS (URL, OBJECTS, POS, CIRCUITS) VALUES (?, ?, ?, ?)', [req.body.url, req.body.objects, req.body.pos, req.body.circuits], (err) => {
        if (err) {
            return next(err);
        }
        res.status(201);
        return res.end();
    });
});

app.post('/circuits', (req, res, next) => {
    DB.run('INSERT INTO CIRCUITS (URL, OBJECTS, POS) VALUES (?, ?, ?)', [req.body.url, req.body.objects, req.body.pos], (err) => {
        if (err) {
            return next(err);
        }
        res.status(201);
        return res.end();
    });
});

app.get('/flats/:id', (req, res, next) => {
    DB.get('SELECT OBJECTS, POS FROM FLATS WHERE ID = ?', [req.params.id], (err, data) => {
        if (err) {
            return next(err);
        }
        return res.json(data);
    });
});

app.get('/circuits/:id', (req, res, next) => {
    DB.get('SELECT OBJECTS, POS FROM CIRCUITS WHERE ID = ?', [req.params.id], (err, data) => {
        if (err) {
            return next(err);
        }
        return res.json(data);
    });
});

app.post('/clic', (req, res, next) => {
    DB.run('INSERT INTO CLIC (URL, OBJECTS, POS) VALUES (?, ?, ?)', [req.body.url, req.body.objects, req.body.pos], (err) => {
        if (err) {
            return next(err);
        }
        res.status(201);
        return res.end();
    });
});

app.put('/players/:id', (req, res, next) => {
    DB.run('UPDATE PLAYERS SET FLAT = ?', [req.params.id], (err) => {
        if(err) {
            return next(err);
        }
        return res.end();
    });
});

app.post('/players', (req, res, next) => {
    DB.run('INSERT INTO PLAYERS (NAME, LEVEL, SCORE, FLAT) VALUES (?, ?, ?, ?)', [req.body.name, req.body.level, req.body.score, req.body.flat], (err) => {
        if (err) {
            return next(err);
        }
        res.status(201);
        return res.end();
    });
});

app.get('/idflats', (req, res, next) => {
    DB.get('SELECT COUNT (*) FROM FLATS', (err, data) => {
        if (err) {
            return next(err);
        }
        return res.json(data);
    });
});

app.get('/idcircuits', (req, res, next) => {
    DB.get('SELECT COUNT (*) FROM CIRCUITS', (err, data) => {
        if (err) {
            return next(err);
        }
        return res.json(data);
    });
});

app.post('/flats/update', (req, res, next) => {
    DB.run('UPDATE FLATS SET OBJECTS = ?, POS = ?, CIRCUITS = ?', [req.body.objects, req.body.pos, req.body.circuits], (err) => {
        if(err) {
            return next(err);
        }
        return res.end();
    });
});

app.post('/circuits/update', (req, res, next) => {
    DB.run('UPDATE CIRCUITS SET OBJECTS = ?, POS = ?', [req.body.objects, req.body.pos], (err) => {
        if(err) {
            return next(err);
        }
        return res.end();
    });
});

app.post('/players/score/:name', (req, res, next) => {
    DB.run('UPDATE PLAYERS SET SCORE = ? WHERE NAME = ?', [req.body.score, req.params.name], (err) => {
        if(err) {
            return next(err);
        }
        return res.end();
    });
});

app.post('/players/level/:name', (req, res, next) => {
    DB.run('UPDATE PLAYERS SET LEVEL = ? WHERE NAME = ?', [req.body.level, req.params.name], (err) => {
        if(err) {
            return next(err);
        }
        return res.end();
    });
});
/************ROUTES*************/

app.listen(3131, (err) => {

    if (err) {
        console.log(err);
    }
    else {
        console.log('app listening on port 3131');
    }
});
