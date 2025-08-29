# HuesarioApp

HuesarioApp es una aplicación móvil multiplataforma desarrollada con .NET MAUI, orientada a la gestión de ventas y piezas de desguace. El proyecto está estructurado para soportar Android, iOS, Windows, MacCatalyst y Tizen.

## Arquitectura y patrón de diseño

La aplicación utiliza el patrón **MVVM (Model-View-ViewModel)**, facilitando la separación de responsabilidades y la escalabilidad del código. Las vistas se encuentran en la carpeta `Views`, los modelos en `Models`, y la lógica de presentación en `ViewModels`.

La inicialización y configuración de servicios se realiza en [`MauiProgram`](MauiProgram.cs), donde se emplea **Inyección de Dependencias** para registrar servicios y ViewModels, siguiendo el principio de inversión de dependencias.

## Principios de código aplicados

- **SOLID**: El código respeta los principios SOLID, especialmente la separación de responsabilidades (SRP) y la inversión de dependencias (DIP) mediante interfaces como [`ICameraServices`](Interfaces/AppServices/ICameraServices.cs) y [`IRepository`](Interfaces/DataServices/IRepository.cs).
- **Desacoplamiento**: Los servicios y la lógica de negocio están desacoplados de la interfaz de usuario, permitiendo pruebas unitarias y facilidad de mantenimiento.
- **Extensibilidad**: El uso de interfaces y clases parciales facilita la extensión de funcionalidades sin modificar el código existente.
- **Multiplataforma**: El proyecto está preparado para ejecutarse en diferentes sistemas operativos, con implementaciones específicas en la carpeta `Platforms`.

## Estructura principal

- **App.xaml / App.xaml.cs**: Configuración principal de la aplicación.
- **AppShell.xaml / AppShell.xaml.cs**: Define la navegación y estructura visual.
- **MauiProgram.cs**: Configuración de servicios y dependencias.
- **Models/**: Entidades y contratos de datos.
- **ViewModels/**: Lógica de presentación y comandos.
- **Views/**: Vistas y páginas de la aplicación.
- **Interfaces/**: Definición de servicios y repositorios.
- **Platforms/**: Código específico para cada plataforma soportada.

## Instalación y ejecución

1. Clona el repositorio.
2. Restaura los paquetes NuGet.
3. Compila y ejecuta el proyecto en la plataforma deseada usando Visual Studio.

## Créditos

Desarrollado por el equipo de Deshuace