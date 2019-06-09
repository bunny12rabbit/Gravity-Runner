using System.IO;
using UnityEngine;
using Zenject;

public class DataManager
{
    [Inject] private ScoreSystem _scoreSystem;

    private IData<ScoreData> _data;

    private string _folderName = "Data";
    private string _fileName = "data.bin";
    private string _path;

    public DataManager()
    {
        _data = new JsonData<ScoreData>();
        _path = Path.Combine(Application.dataPath, _folderName);

    }

    public void Save()
    {
        if (!Directory.Exists(Path.Combine(_path)))
        {
            Directory.CreateDirectory(_path);
        }

        var scoreData = new ScoreData
        {
            highScore = _scoreSystem.HighScore
        };

        _data.Save(scoreData, Path.Combine(_path, _fileName));
    }

    public void Load()
    {
        var file = Path.Combine(_path, _fileName);
        if (!File.Exists(file)) return;
        var newData = _data.Load(file);
        _scoreSystem.HighScore = newData.highScore;
    }
}