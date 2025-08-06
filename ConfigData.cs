using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

public class ConfigData
{
    public string futures { get; set; }
    public string proxy { get; set; }
    public int port { get; set; }       // ✅ 新增的字段
    public bool startup { get; set; }
    public int timeout { get; set; }
}


public static class YamlConfigLoader
{
    public static ConfigData LoadConfig()
    {
        string exePath = AppDomain.CurrentDomain.BaseDirectory;
        string yamlPath = Path.Combine(exePath, "config.yaml");

        if (!File.Exists(yamlPath))
        {
            throw new FileNotFoundException("未找到配置文件: config.yaml");
        }

        string yaml = File.ReadAllText(yamlPath);

        var deserializer = new DeserializerBuilder().Build();
        var config = deserializer.Deserialize<ConfigData>(yaml);

        return config;
    }
}