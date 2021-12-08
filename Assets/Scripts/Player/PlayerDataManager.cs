using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataManager
{
    private const string PROGRESS_KEY = "Progress";

    public static PlayerData player = new PlayerData();
    public static void Load()
    {
        // Cek apakah ada data yang tersimpan sebagai PROGRESS_KEY
        if (!PlayerPrefs.HasKey(PROGRESS_KEY))
        {
            // Jika tidak ada, maka simpan data baru
            Save();
        }
        else
        {
            // Jika ada, maka timpa progress dengan yang sebelumnya
            string json = PlayerPrefs.GetString(PROGRESS_KEY);
            player = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(player);
        PlayerPrefs.SetString(PROGRESS_KEY, json);
    }
}
