using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.IO;
using System.Collections.Generic;

public class DatabaseService : MonoBehaviour
{

    private IDbConnection dbconnection;
    private static DatabaseService singletone;

    public static void Init()
    {
        singletone = new DatabaseService();
        singletone.Open();
    }


    // Use this for initialization
    void Start()
    {
        Init();
        GameObject.Find("GameController").GetComponent<Game>().SelectUnsuitablePhrase();
    }

    public void Open()
    {
        string connectionString;
        if (Application.platform == RuntimePlatform.Android)
        {
            connectionString = Application.persistentDataPath + "/DataBase.db";
            if (!File.Exists(connectionString))
            {
                WWW load = new WWW(Application.streamingAssetsPath + "!/assets/DataBase.db");
                while (!load.isDone) { }
                File.WriteAllBytes(connectionString, load.bytes);
            }
        }
        else
            connectionString = Application.streamingAssetsPath + "/DataBase.db";
        dbconnection = (IDbConnection)new SqliteConnection("URI=file:" + connectionString);
        try
        {
            dbconnection.Open();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            dbconnection.Close();
        }
    }

    public void Close()
    {
        dbconnection.Close();
    }

    public static List<List<KeyValuePair<string, object>>> ExecuteCommand(string sql)
    {
        if (String.IsNullOrEmpty(sql))
            return null;
        List<List<KeyValuePair<string, object>>> result = new List<List<KeyValuePair<string, object>>>();
        using (IDbCommand dbcmd = singletone.dbconnection.CreateCommand())
        {
            dbcmd.CommandText = sql;
            using (IDataReader reader = dbcmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    List<KeyValuePair<string, object>> row = new List<KeyValuePair<string, object>>();
                    for (int col = 0; col < reader.FieldCount; col++)
                    {
                        switch (reader.GetFieldType(col).ToString())
                        {
                            case "System.Int64":
                                row.Add(new KeyValuePair<string, object>(reader.GetName(col), reader.GetInt64(col)));
                                break;
                            case "System.String":
                                row.Add(new KeyValuePair<string, object>(reader.GetName(col), reader.GetString(col)));
                                break;
                        }
                        
                    }
                    result.Add(row);
                }
            }
        }
        return result;
    }
}