# Note-net  
开放源码的个人笔记网站（新手实用）  
数据库自己建，换电脑记得搬运数据  
这个项目是.net Core项目，用vs运行，是本人初入编程世界编写的MVC网站，虽然代码很烂，但是可以用于给自己分类做笔记，很实用。  
1.要使用本项目做笔记需要下载sql server(我的版本是2012)，仅需创建一个名为"maoxianshiyanshi"的数据库，并创建一个名为user的表。  
1.2.user表里的数据类型是 id -int, name -varchar(MAX), mima -varchar(MAX), tables -varchar(MAX)。  
2.修改数据库字符串为自己的数据库字符串，在/gerenlianxisheng/gerenlianxisheng/appsettings.json 中，修改第一行的value值即可。  
完成以上步骤运行网站，注册账号即可使用（账号信息存在自己建的user表里）
