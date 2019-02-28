using UnityEngine;
using System.Collections;

public class InsertData : MonoBehaviour {
    private string secretKey = "mySecretKey";
    // the id of the current player
    public int lastId;
    // indicate if the name and age are set
    public bool set = false;
    // indicate if the data has been send
    public bool send = false;
    // used to get the requered php for sending or updating the answers.
    private string PostUserURL = "http://languagelabgames.webhosting.rug.nl/taalgame/user.php?"; // insert name and age
    private string PostAnswersURL = "http://languagelabgames.webhosting.rug.nl/taalgame/answers.php?"; // insert/update answers

    // set the data you want to sent in the stages script
    // will only send data if conditions are met
    public void SetData(string name, int age) {
        if (send == false && set == true) {
            // send name and age
            StartCoroutine(PostNameAge(name, age));
            // indicate the data has been send
            send = true;
        }
    }

    // set the data you want to update in the stages script
    public void UpdateData(int level, int Question, string Answer) {
        // check if a name is set
        if (set == true) {
            // send the data to the id attached to the name
            StartCoroutine(PostAnswers(level,Question, Answer));
        }
    }

    // post user info 
    // input the users name and age
    IEnumerator PostNameAge(string name, int age) {

        string hash = Md5Sum(name + age + secretKey);

        // sending the answers
        string post_url = PostUserURL + "&Naam=" + WWW.EscapeURL(name) + "&Leeftijd=" + age + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        // get the last id from the php script and then set the last id
        int.TryParse(hs_post.text.ToString(), out lastId);

        if (hs_post.error != null) {
            print("There was an error posting the answers: " + hs_post.error);
        }
    }

    // post answers
    // input the level, the question number and the given answer
    IEnumerator PostAnswers(int level, int Question, string Answer) {
        // updating the answers
        string post_url = PostAnswersURL + "&User_id=" + lastId + "&Level=" + level + "&Question=" + Question + "&Answer=" + WWW.EscapeURL(Answer);

        // Post the URL to the site and create a download object to get the result
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done
 
        if (hs_post.error != null) {
            print("There was an error posting the answers: " + hs_post.error);
        }
    }

    public string Md5Sum(string strToEncrypt) {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++) {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
