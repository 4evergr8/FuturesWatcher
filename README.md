# FuturesWatcher 👀

一个用于实时监控数字货币期货价格的小巧悬浮窗工具。

> ⚡ 轻量 · 无边框 · 可拖动 · 自动更新 · 自定义颜色

---

## ✨ 功能特色

- 💹 实时获取期货标的（如 BTC/USDT）的价格
- 📡 自动轮询远程接口，不断刷新数据
- 🪟 悬浮窗显示在屏幕右下角，支持拖动
- 🎨 数字颜色根据涨跌自动切换（红/绿）
- 🧊 全透明窗口，仅显示数字本身，极简美观
- 🧮 支持保留两位小数，自动格式化显示

---

## 📦 项目结构

```

FuturesWatcher/
├── Program.cs                // 启动入口
├── FloatingNumberForm.cs     // 悬浮窗主窗体逻辑
├── HttpFetcher.cs           // 网络请求与数据处理
├── App.config               // 可选：配置接口地址等参数
└── README.md

```

---

## 🚀 快速开始

1. 使用 Visual Studio 打开项目
2. 运行 `Program.cs`
3. 默认会在屏幕右下角显示一个悬浮数字
4. 通过代码中配置的 API 接口自动获取行情数据并刷新

---

## 🛠️ 自定义配置

你可以修改以下内容来自定义行为：

- **刷新间隔**：设置轮询频率（例如 2 秒一次）
- **数据源接口**：例如来自币安（Binance）的 `https://fapi.binance.com/fapi/v1/premiumIndex`
- **显示格式**：控制保留几位小数
- **悬浮窗位置和大小**：在 `FloatingNumberForm.cs` 里修改 `Size` 和 `Location`

---

## 🧪 截图

> 全透明背景，仅数字悬浮，简洁直观

![screenshot](./screenshot.png)

---

## 📋 License

MIT License

---

## ❤️ 致谢

本项目灵感来自对数字货币价格的实时监控需求，结合 .NET WinForms 的极简 UI 实现。

