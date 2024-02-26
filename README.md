# Documentación de la API

## Bienvenido a la documentación de la API de MediaLoggerAPI.

Esta API proporciona acceso a varios recursos para interactuar con el servidor, permitiendo así el envío de videos y logs al servidor.

## Base URL

La base URL para acceder a la API es: `https://apimedialogs.e-city.co`

## Tipos de Respuesta

- **Success**: Éxito en la solicitud.
- **Bad Request**: Error en la solicitud del cliente.
- **Unauthorized**: Acceso no autorizado.
- **Server Error**: Error interno del servidor.

## Endpoints Disponibles

A continuación, se enumeran los endpoints disponibles junto con una breve descripción y ejemplos de cómo consumirlos.

### Endpoint 1: /Auth/Login

- **Descripción**: Este endpoint se utiliza para autenticarse y obtener un token de acceso para acceder a otros recursos protegidos.
  
- **Método HTTP**: POST
  
- **Parámetros**:

  - **Header**: 
    - `API-KEY-LOGIN` (tipo string)
  
  - **Body**:  
    - `userName` (tipo string)
    - `password` (tipo string)
        
- **Ejemplo de solicitud**:

  ```bash
  curl -X 'POST' \
    'https://apimedialogs.e-city.co/Auth/Login' \
    -H 'accept: application/json' \
    -H 'API-KEY-LOGIN: 123' \
    -H 'Content-Type: application/json' \
    -d '{
          "userName": "string",
          "password": "string"
        }'
- **Ejemplo de respuesta**:
  
  ```bash
  {
    "statusCode": 200,
    "message": "Login has been successfully",
    "response": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlBheSsgUHJ1ZWJhMSIsIm5iZiI6MTcwNzgyOTUxMiwiZXhwIjoxNzA3ODMzMTEyLCJpYXQiOjE3MDc4Mjk1MTJ9.bxrItCQePKfRArISGjDlqptI1N7f5RtgV9W4FSBCBDQ"
  }

  
### Endpoint 2: /MediaLogger/SaveLog

- **Descripción**: Este endpoint se utiliza para Guardar Logs en el servidor.
  
- **Método HTTP**: POST
  
- **Parámetros**:

  - **Header**: 
    - `JWT` (tipo string)
  
  - **Body**:  
    - `logtype` (tipo string)
    - `content` (tipo string)
        
- **Ejemplo de solicitud**:

  ```bash
  curl -X 'POST' \
  'https://apimedialogs.e-city.co/MediaLogger/SaveLog' \
  -H 'accept: application/json' \
  -H 'JWT: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlBheSsgUHJ1ZWJhMSIsIm5iZiI6MTcwNzgyOTUxMiwiZXhwIjoxNzA3ODMzMTEyLCJpYXQiOjE3MDc4Mjk1MTJ9.bxrItCQePKfRArISGjDlqptI1N7f5RtgV9W4FSBCBDQ' \
  -H 'Content-Type: multipart/form-data' \
  -d '{
        "logtype":  "string",
        "content": "string"
      }'
  
- **Ejemplo de respuesta**:
  
  ```bash
  {
    "statusCode": 200,
    "message": "Log sent has been successfully",
    "response": null
  }
  
### Endpoint 3: /MediaRetriver/DownloadLog
- **Descripción**: Este endpoint se utiliza para descargar logs del servidor.
  
- **Método HTTP**: POST
  
- **Parámetros**:

  - **Header**: 
    - `JWT` (tipo string)
  
  - **Body**:
    - `idPaypad` (tipo int)
    - `startDate` (tipo DateTime)
    - `finalDate` (tipo DateTime)
        
- **Ejemplo de solicitud**:

  ```bash
    curl -X 'POST' \
      'https://apimedialogs.e-city.co/MediaRetriver/DownloadLog' \
      -H 'accept: application/json' \
      -H 'JWT: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlBheSsgUHJ1ZWJhMSIsIm5iZiI6MTcwNzgyOTUxMiwiZXhwIjoxNzA3ODMzMTEyLCJpYXQiOjE3MDc4Mjk1MTJ9.bxrItCQePKfRArISGjDlqptI1N7f5RtgV9W4FSBCBDQ' \
      -H 'Content-Type: application/json' \
      -d '{
          "idPaypad": 1005,
          "startDate": "2024-02-23T13:35:16.151Z",
          "finalDate": "2024-02-26T13:35:16.151Z"
        }'
  
- **Ejemplo de respuesta**:
  
  ```bash
   access-control-allow-origin: * 
   content-disposition: attachment; filename="log-Pay Prueba1-del-2024/02/23-al-2024/02/26"; filename*=UTF-8''log-Pay%20Prueba1-del-2024%2F02%2F23-al-2024%2F02%2F26 
   content-length: 244 
   content-type: text/plain 
   date: Mon,26 Feb 2024 13:39:44 GMT 
   server: Kestrel 
