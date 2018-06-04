using System.Text;
using System.Text.RegularExpressions;


public class PasswordAdvisor
{
	public static int CheckStrength(string password)
	{
		int score = 0;

		if (password.Length < 1)
			return 0;
		if (password.Length < 4)
			return 1;

		if (password.Length >= 4)
			score++;
		if (password.Length >= 8)
			score++;
		if (password.Length >= 12)
			score++;
		if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
			score++;
		if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
			Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success) //Vérifie s'il y a minuscules et majuscules
			score++;
		if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
			score++;

		if (score > 3)
			return 3;
		return score;
	}
}