const DB_NAME = 'test_express_sqlite';
const Fs = require('fs');
const Path = require ('path');
const Sql = require ('sqlite3');
const Express = require('express');
const BP = require('body-parser');

const app = Express();

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

app.get('/', (req, res, next) => {

    DB.all('SELECT * FROM USERS', (err, data) => {

        if (err) {
            return next(err);
        }
        return res.json(data);
    });
});

app.get('/:username', (req, res, next) => {

    DB.get('SELECT * FROM USERS WHERE USERNAME = ?', [req.params.username], (err, data) => {

        if (err) {
            return next(err);
        }
        return res.json(data);
    });
});

app.post('/', (req, res, next) => {

    DB.run('INSERT INTO USERS (USERNAME, PASSWORD) VALUES (?, ?)', [req.body.username, req.body.password], (err) => {
        if (err) {
            return next(err);
        }
        res.status(201);
        console.log(req.body.username, req.body.password);
        return res.end();
    });
});

app.listen(3131, (err) => {

    if (err) {
        console.log(err);
    }
    else {
        console.log('app listening on port 3131');
    }
});
