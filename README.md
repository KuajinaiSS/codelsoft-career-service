# codelsoft-career-service
Este microservicio usa el puerto **5002**, necesitas tenerlo habitado para usar este microservicio (localhost:5002)
### Requisitos previos (por confirmar)

- SDK [.NET8](https://dotnet.microsoft.com/es-es/download/dotnet/8.0).
- EF CLI [EF CLI](https://www.nuget.org/packages/dotnet-ef/).
- Git [2.33.0](https://git-scm.com/downloads) o superior.

### Pasos para clonar

1. Clona el repositorio:
```bash
git clone https://github.com/KuajinaiSS/codelsoft-career-service.git
```

2. Acederemos a la carpeta:
```bash
cd codelsoft-career-service
```

3. accederemos con nuestro editor de codigo/ide preferido

4. en el terminal, instalaremos las dependendencias
```bash
dotnet restore
```

5. Clonamos el .env.example all .env y lo llenamos con las credenciales nuestras de nuestra base de datos en SQLServer
```bash
cd career-service
copy .env.example .env
```

6. Creamos las migraciones 
```bash
dotnet ef migrations add Initial
```

7. Migramos a la base de datos
```bash
dotnet ef database update
```

8. Ejecutamos el proyecto
```bash
dotnet run
```


