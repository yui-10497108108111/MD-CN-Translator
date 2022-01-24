# MD-CN-Translator

实时翻译Master Duel 选中卡片文本。两种方式内存和图像识别。[下载](https://github.com/yui-10497108108111/MD-CN-Translator/releases/tag/0.0.4)     
图像识别方式跟内存读取在使用体验上基本一致，参考[GIF](Image/demo.gif)，左边的就是图像识别方式的，GIF速度被慢放了点，实际上没有那么大差距，不过在名字过长的卡片上图像识别不太灵敏。    
图像识别方式的构建和使用有点难度，[查看文档](MDT-OCR/README.md)    

> 补充一下，最后这个项目从功能上已经很完善了，我知道问题还是有的。之后看情况吧。    
如果有人用遇到问题请提Issues,我会抽时间修复的。

<!-- PROJECT SHIELDS -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![GPLv3.0 License][license-shield]][license-url]

 
## 目录

  - [开发前的配置要求](#开发前的配置要求)
  - [安装步骤](#安装步骤)
  - [如何参与开源项目](#如何参与开源项目)
- [版本控制](#版本控制)
- [作者](#作者)
- [鸣谢](#鸣谢)


#### 开发前的配置要求

1. visual studio 2022

#### **安装步骤**

1. 克隆仓库
```sh
git clone --recursive https://github.com/yui-10497108108111/MD-CN-Translator
```
2. 打开 MDT.sln
3. 使用包管理控制台
```sh
Update-Package -reinstall 
```
4. 更改Rewrite的目标框架为.Net Framework 4.7.2
5. 生成 x64 Project
6. 复制 Database/cards.json 到生成的二进制文件目录。
#### 使用说明
* 默认窗口不可点击、拖动。按住左ALT之后才可以点击，拖动。





#### 如何参与开源项目

贡献使开源社区成为一个学习、激励和创造的绝佳场所。你所作的任何贡献都是**非常感谢**的。


1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



### 版本控制

该项目使用Git进行版本管理。您可以在repository参看当前可用版本。

### 作者

yui

 *您也可以在贡献者名单中参看所有参与该项目的开发者。*

### 版权说明

该项目签署了GPL3.0 授权许可，详情请参阅 [LICENSE.txt](https://github.com/yui-10497108108111/MD-CN-Translator/blob/main/LICENSE)    
特别说明：引用的部分（包括但不限于代码，图片，数据库），版权归原作者所有。

### 鸣谢


- [JokinsRewrite](https://github.com/JokinAce/JokinsRewrite)
- [EasyOCR](https://github.com/JaidedAI/EasyOCR)
- [ygopro-database](https://github.com/mycard/ygopro-database)

<!-- links -->
[your-project-path]:yui-10497108108111/MD-CN-Translator
[contributors-shield]: https://img.shields.io/github/contributors/yui-10497108108111/MD-CN-Translator.svg?style=flat-square
[contributors-url]: https://github.com/yui-10497108108111/MD-CN-Translator/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/yui-10497108108111/MD-CN-Translator.svg?style=flat-square
[forks-url]: https://github.com/yui-10497108108111/MD-CN-Translator/network/members
[stars-shield]: https://img.shields.io/github/stars/yui-10497108108111/MD-CN-Translator.svg?style=flat-square
[stars-url]: https://github.com/yui-10497108108111/MD-CN-Translator/stargazers
[issues-shield]: https://img.shields.io/github/issues/yui-10497108108111/MD-CN-Translator.svg?style=flat-square
[issues-url]: https://img.shields.io/github/issues/yui-10497108108111/MD-CN-Translator.svg
[license-shield]: https://img.shields.io/github/license/yui-10497108108111/MD-CN-Translator.svg?style=flat-square
[license-url]: https://github.com/yui-10497108108111/MD-CN-Translator/blob/master/LICENSE.txt



