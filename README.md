# Change Log
## Validation
### User Login & Registration  

Login (Development) :    
**- email salah, password betul**
```
Request : 
{
  "email": "jok@gmail.com",
  "password": "Passw0rd"
}

Respond : 
{
    "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
    "title": "Not Found",
    "status": 404,
    "traceId": "00-578c2e12ff3257beafdedc65078ed61b-b374fc93b768ce8b-00"
}
```
**- email betul, password salah**
```
Request : 
{
  "email": "joko@gmail.com",
  "password": "Pasw0rd"
}

Respond : 
{
    "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
    "title": "Unauthorized",
    "status": 401,
    "traceId": "00-578c2e12ff3257beafdedc65078ed61b-b374fc93b768ce8b-00"
}
```

Register (Development) :  
**- Semua normal, Tetapi email sudah ada**
```
Request : 
{
  "email": "joko@gmail.com",
  "username": "test",
  "displayname": "test",
  "password": "Passw0rd"
}

Response : 
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-a355d0b6040a94f3fd894bd358114f9b-af398d9a81544e3e-00",
    "errors": {
        "email": [
            "Email Already Taken"
        ]
    }
}
```
**- Semua normal, Tetapi uesrname sudah ada**
```
Request : 
{
  "email": "test@gmail.com",
  "username": "joko",
  "displayname": "test",
  "password": "Passw0rd"
}

Response : 
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-a355d0b6040a94f3fd894bd358114f9b-af398d9a81544e3e-00",
    "errors": {
        "username": [
            "Username Already Taken"
        ]
    }
}
```
**- Semua normal, Tetapi password tidak memenuhi Ketentuan**
```
Request : 
{
  "email": "joko@gmail.com",
  "username": "test",
  "displayname": "test",
  "password": "Passw0rd"
}

Response : 
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-a355d0b6040a94f3fd894bd358114f9b-af398d9a81544e3e-00",
    "errors": {
        "password": [
            "message"
        ]
    }
}
```  
**Note :**  
    **"message"** akan berubah sesuai error yang diperlukan
    Ketentuan Password :  
    -) Harus mempunyai >= 8 digit dan  <= 16 digit  
    -) Tidak boleh cuman spasi  
    -) Harus ada setidaknya 1 huruf besar  
    -) Harus ada setidaknya 1 angka    
### NotFound 404  
Semua yang tidak ada dalam database. seperti movie, room, dll. maka akan memperoleh 404 message "Not Found"  
## Entity Update  
### Movie and Room
Check Swagger UI :)  
### More Seeded Data
