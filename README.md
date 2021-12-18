## DotNet User Custom
### Esse projeto tem como objetivo didático, considere eventuais problemas ou falta de técnica, solicitar um pull request, obrigado :)

#### Aplicação web com .net core(5) MVC + identity framework com add de informações ao usuário.
#### Nesse exemplo usaremos login direto, sem a necessidade de comfirmação de email, ou autenticação de dois fatores

#### pacotes instalandos
* Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.x
* Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.x
* Install-Package Microsoft.EntityFrameworkCore.Design -Version 5.0.x

Nem todos os arquivos são necessários, caso deseje apenas adicionar campos em AspNetUser considere marcar os que estão com sinal de mais '+'

#### Add Identity config
* _Layout.cshtml (Layout padrão)
* Account/Manage/Change Password
* Account/Manage/PersonalData
* Account/AccessDenied
* Account/Lockout
* Account/Manage/ManageNav
* Account/manage/DeletePersonalData
* Account/Manage/Email
* Account/ForgotPassword
* Account/Login +
* Account/Logout
* Account/Manage/StatusMessage
* Account/Nabage/Index +
* Account/Register +

Classe de contexto e classe de usuário são criados clicando no sinal de '+'
Caso ocorra algum erro na criação dos arquivos, tente novamente a criação do mesmo em seguida, caso erro persista, considere pesquisar o problema
