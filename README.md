# TodoList App - .NET 6 Console & Web API + Vue Frontend

Proyecto realizado por **Nicolás Corredor** para el proceso técnico de la empresa **IT Beyond IO Oxigen**.

## 🧩 Estructura del Proyecto

- `TodoList.API/` → Web API en .NET 6 con lógica de negocio y validaciones
- `TodoList.Tests/` → Tests unitarios usando xUnit
- `vue-todolist/` → Frontend en Vue 3 con Axios y barra de progreso

---

## 🛠 Requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Node.js (recomendado v18+)
- Visual Studio 2022 o Visual Studio Code

---

## 🚀 Instrucciones para ejecución

### 1. Clonar o descargar el proyecto

```bash
git clone https://github.com/cogu92/TodoListApp.git
cd TodoListApp
```

### 2. Cambiar el SDK si es necesario

```bash
dotnet new globaljson --sdk-version 6.0.101
```

### 3. Restaurar y compilar

```bash
dotnet restore
dotnet build
```

### 4. Ejecutar la Web API

```bash
cd TodoList.API
dotnet run
```

Abrir en navegador: [http://localhost:44338/swagger](http://localhost:44338/swagger)

---

### 5. Ejecutar tests

```bash
cd ../TodoList.Tests
dotnet test
```

---

### 6. Ejecutar Frontend

```bash
cd ../vue-todolist
npm install
npm run dev
```

Accede a: [http://localhost:5173](http://localhost:5173)

---

## 📌 Notas

- Cumple con los requisitos obligatorios y extra del ejercicio técnico.
- Se desarrolló una API REST moderna + cliente frontend interactivo.
- Los tests cubren caminos felices y casos de error importantes.

---
