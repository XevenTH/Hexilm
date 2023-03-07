# CoolMovie

### Kontribusi
  &nbsp;&nbsp;&nbsp;&nbsp; Kami sangat terbuka terhadap kontribusi dari para developer yang ingin ikut berpartisipasi dalam pengembangan CoolMovie. Silahkan untuk membuat pull request dengan perubahan atau penambahan fitur baru. Khususnya di frontend tetapi tak terkecuali de backend.

  &nbsp;&nbsp;&nbsp;&nbsp; Silahkan melakukan Forking atau Git clone untuk menjalan app secara local.
  
### Struktur Folder  
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CoolMovie menggunakan Clean Architecture (By Uncle Bob) yang memungkinkan data masuk dan keluar tanpa &nbsp;&nbsp;&nbsp;&nbsp;tau menau tentang teknologi apa yang sedang memanggil mereka dan mementingkan abstraction dan &nbsp;&nbsp;&nbsp;&nbsp;encapsulation. Sehingga kita bisa menggunakan dua &nbsp;&nbsp;&nbsp;&nbsp;teknologi berbeda yaitu ASP.NET Core dan React.
1. **./Client**  
Merupakan project React yang digunakan untuk menerima response dan melakukan request dari API. Projek ini menggunakan MobX sebagai (state management) serta TailwindCSS sebagai styling.
2. **./API**  
Merupakan project ASP.NET Core yang digunakan untuk menerima request serta memberikan response kepada clinet-side. Projek ini juga menggunakan EF Core serta beberapa package seperti MediatR dan juga AutoMapper.
