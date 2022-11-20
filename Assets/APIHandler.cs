using UnityEngine;
using System.Net;
using System.IO;

public static class APIHandler
{
    public static ResultData getResult(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://aprab.pythonanywhere.com/predict/"+url);
        HttpWebResponse response= (HttpWebResponse)request.GetResponse();
        //read the response
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<ResultData>(json);
    }
}
