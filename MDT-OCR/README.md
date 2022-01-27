# 使用说明
确保ocr_api.exe已经启动，然后启动MDT-OCR.exe即可。          
内存与OCR版本的UI保持一致。        
启动后有2个窗口，按住左ALT把小窗口调整到合适大小覆盖到卡片名字的位置即可。       
已适配任何分辨率，但不能让其他窗口遮挡卡片名字。      
如需要自己构建看下面的步骤。

# 开发前的配置要求
1. python3.7
1. vs2022
# 构建步骤
1. 克隆仓库
```sh
git clone --recursive https://github.com/yui-10497108108111/MD-CN-Translator
cd MD-CN-Translator
```

1. 安装依赖

```sh
cd MDT-OCR-PY
pip install -r requirements.txt
```

1. 运行ocr_api.py
```sh
python ocr_api.py
```
1. 打开 MDT.sln
1. 使用包管理控制台
```sh
Update-Package -reinstall 
```
1. 生成 MDT-OCR Project
1. 复制 Database/cards.json 到生成的二进制文件目录。
1. 运行MDT-OCR.EXE

# 注意事项
1. 自动检测CPU占用极高。
1. 可以把torch切换到GPU版本。https://pytorch.org
1. 名字太长的卡需要滚动一下之后在检测。
