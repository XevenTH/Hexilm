# CoolMovie

## Kontribusi
  &nbsp;&nbsp;&nbsp;&nbsp; Kami sangat terbuka terhadap kontribusi dari para developer yang ingin ikut berpartisipasi dalam pengembangan CoolMovie. Silahkan untuk membuat pull request dengan perubahan atau penambahan fitur baru. Khususnya di frontend tetapi tak terkecuali de backend.

  &nbsp;&nbsp;&nbsp;&nbsp; Silahkan melakukan Forking atau Git clone untuk menjalan app secara local.

## Struktur Folder  

  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CoolMovie menggunakan Clean Architecture (By Uncle Bob) yang memungkinkan data masuk dan keluar tanpa &nbsp;&nbsp;&nbsp;&nbsp;tau menau tentang teknologi apa yang sedang memanggil mereka dan mementingkan abstraction dan &nbsp;&nbsp;&nbsp;&nbsp;encapsulation. Sehingga kita bisa menggunakan dua teknologi berbeda yaitu ASP.NET Core dan React.
1. **./Client**  
Merupakan project React yang digunakan untuk menerima response dan melakukan request dari API. Projek ini menggunakan MobX sebagai (state management) serta TailwindCSS sebagai styling.
2. **./API**  
Merupakan project ASP.NET Core yang digunakan untuk menerima request serta memberikan response kepada clinet-side. Projek ini juga menggunakan EF Core serta beberapa package seperti MediatR dan juga AutoMapper 

## Instalasi dan Setup .NET
&nbsp;&nbsp;&nbsp;&nbsp; Karena project ini menggunakan C#, API harus berjalan pada .NET Environtment. Silahkan mendownload sesuai step dan OS seperti yabg ada di bawah ini.
### Windows
1. Lakukan Fork/Clone Pada project ini.
2. https://dotnet.microsoft.com/en-us/download. Download versi terakhir dari .NET, Lalu install.
3. Masuk ke root folder.
4. Buka terminal ketikan 
   ```cmd
   1. dotnet clean 
   2. dotnet build 
   3. dotnet restore
   ```
5. Masuk ke folder./API. Lalu ketikan dotnet watch run.
6. Maka akan terbuka swagger UI untuk list API.
### Linux
1. Lakukan Fork/Clone Pada project ini.
2. Ikut langkah - langkah di website https://www.linode.com/docs/guides/tutorial-host-asp-net-core-on-linux/.
3. Pastikan saat command "sudo apt install dotnet-sdk-5.0" gunakan "sudo apt install dotnet-sdk-7.0".
4. Masuk ke root folder.
5. Buka terminal ketikan 
   ```cmd
   1. dotnet clean 
   2. dotnet build 
   3. dotnet restore
   ```
5. Masuk ke folder./API. Lalu ketikan dotnet watch run.
6. Maka akan terbuka swagger UI untuk list API.
### MacOS
&nbsp;&nbsp;&nbsp;&nbsp; Searching....
