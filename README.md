# HuesarioApp - Aplicación Móvil Multiplataforma

Aplicación móvil desarrollada con .NET MAUI para gestión de inventario y ventas. Demuestra implementación de patrones de
diseño modernos, arquitectura MVVM y desarrollo multiplataforma.

## Características Técnicas

- **Arquitectura MVVM** con separación clara de responsabilidades
- **Inyección de Dependencias** para desacoplamiento de servicios
- **Base de Datos Local** con SQLite y operaciones CRUD
- **Servicios Multiplataforma** para funcionalidades nativas
- **Sistema de Validación** con interfaces y validadores específicos
- **Captura de Fotos** integrada con permisos y manejo de errores

## Arquitectura y Patrones

### Principios SOLID Implementados

- **SRP**: Separación de responsabilidades en capas
- **OCP**: Extensibilidad mediante interfaces
- **LSP**: Implementaciones que respetan contratos
- **ISP**: Interfaces específicas y bien definidas
- **DIP**: Inversión de dependencias con DI

### Patrones de Diseño

- **Repository Pattern** para acceso a datos
- **Command Pattern** para acciones de UI
- **Observer Pattern** con INotifyPropertyChanged
- **Dependency Injection** para gestión de servicios

## Estructura principal

- **App.xaml / App.xaml.cs**: Configuración principal de la aplicación.
- **AppShell.xaml / AppShell.xaml.cs**: Define la navegación y estructura visual.
- **MauiProgram.cs**: Configuración de servicios y dependencias.
- **Models/**: Entidades y contratos de datos.
- **ViewModels/**: Lógica de presentación y comandos.
- **Views/**: Vistas y páginas de la aplicación.
- **Interfaces/**: Definición de servicios y repositorios.
- **Platforms/**: Código específico para cada plataforma soportada.

## Stack Tecnológico

- **.NET 9.0 MAUI** - Framework multiplataforma
- **SQLite** - Persistencia de datos local
- **CommunityToolkit.Mvvm** - Implementación MVVM
- **XAML** - Interfaces de usuario declarativas
-
- ## Plataformas Soportadas

- Android (API 21+)
- iOS (15.0+)
- Windows (10.0.17763.0+)
- MacCatalyst (15.0+)

## Instalación y ejecución

1. Clona el repositorio.
2. Restaura los paquetes NuGet.
3. Compila y ejecuta el proyecto en la plataforma deseada usando Visual Studio.

## Créditos

Desarrollado por el equipo de Deshuace