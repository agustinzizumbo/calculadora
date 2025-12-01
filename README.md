# 🚀 Calculadora KW - Sistema Completo (Backend + Frontend + Base de Datos)

Una solución integral para calcular consumo de energía de electrodomésticos. Ejecuta todo localmente con un solo comando.

## 📋 Requisitos

Antes de empezar, asegúrate de tener instalado:

- **Docker Desktop** (incluye Docker y Docker Compose)
  - Windows/Mac: https://www.docker.com/products/docker-desktop
  - Linux: `sudo apt-get install docker-ce docker-compose-plugin`
- **Git** (opcional, para clonar el repositorio)

## 🚀 Inicio Rápido (3 pasos)

### 1️⃣ Clonar o descargar el proyecto

```bash
git clone https://github.com/agustinzizumbo/calculadora.git
cd calculadora/CalculadoraKW.Api
```

### 2️⃣ Ejecutar con Docker Compose

```bash
docker-compose up -d
```

**¿Qué está pasando?**
- Docker descarga las imágenes necesarias (primera vez ~5-10 minutos)
- Levanta 3 contenedores:
  - **API Backend** (.NET) en `http://localhost:5001`
  - **Frontend** (Angular) en `http://localhost:8081`
  - **Base de Datos** (SQL Server) en `localhost:1433`

### 3️⃣ Acceder a la aplicación

- 🌐 **Frontend (Interfaz)**: http://localhost:8081
- 📡 **API (Backend)**: http://localhost:5001
- 📚 **Swagger (Documentación API)**: http://localhost:5001/swagger

## ⚙️ Configuración

El archivo `.env` en la raíz contiene la configuración:

```env
API_PORT=5001              # Puerto del backend
FRONTEND_PORT=8081        # Puerto del frontend
DB_PORT=1433              # Puerto de SQL Server
DB_PASSWORD=Admin@1234    # Contraseña de BD (cambiar para producción)
```

**Para cambiar puertos o contraseña:**
1. Edita `.env`
2. Ejecuta `docker-compose down && docker-compose up -d`

## 📋 Comandos Útiles

### Ver estado de los contenedores
```bash
docker-compose ps
```

### Ver logs de la API
```bash
docker-compose logs api -f
```

### Ver logs del frontend
```bash
docker-compose logs frontend -f
```

### Ver logs de la base de datos
```bash
docker-compose logs db -f
```

### Detener todo
```bash
docker-compose down
```

### Detener y eliminar volúmenes (limpia la BD también)
```bash
docker-compose down -v
```

### Reconstruir las imágenes
```bash
docker-compose build --no-cache
docker-compose up -d
```

## 🔧 Solución de Problemas

### ❌ "Port 5001 is already allocated"
Otro servicio está usando el puerto. Cambia en `.env`:
```env
API_PORT=5002  # Usa 5002 o cualquier puerto libre
```

### ❌ "Cannot connect to Docker daemon"
- Asegúrate de que **Docker Desktop está abierto y corriendo**
- En Linux: `sudo systemctl start docker`

### ❌ El frontend no se conecta al backend
- Verifica que ambos contenedores estén corriendo: `docker-compose ps`
- Revisa los logs: `docker-compose logs api` y `docker-compose logs frontend`
- Limpia y reinicia: `docker-compose down && docker-compose up -d`

### ❌ La base de datos tarda mucho en iniciar
- Esto es normal la primera vez (descarga imagen ~2GB)
- Espera a que todos los contenedores muestren `Up` en `docker-compose ps`

## 📊 Arquitectura

```
┌─────────────────────────────────────────────┐
│  Tu Navegador (http://localhost:8081)       │
│  ┌───────────────────────────────────────┐  │
│  │  Frontend Angular (Nginx)             │  │
│  │  - UI para registrar aparatos         │  │
│  │  - Cálculo de consumos                │  │
│  │  - Gráficas                           │  │
│  └───────────────────────────────────────┘  │
└─────────────────────────────────────────────┘
          ↓ (peticiones HTTP/CORS)
┌─────────────────────────────────────────────┐
│  Backend API (.NET 9)                       │
│  ┌───────────────────────────────────────┐  │
│  │  http://localhost:5001/api/*          │  │
│  │  - Endpoints: Aparatos, UsoAparatos   │  │
│  │  - AutoMapper (DTOs)                  │  │
│  │  - Validación                         │  │
│  └───────────────────────────────────────┘  │
└─────────────────────────────────────────────┘
          ↓ (Entity Framework)
┌─────────────────────────────────────────────┐
│  SQL Server (localhost:1433)                │
│  ┌───────────────────────────────────────┐  │
│  │  Base de Datos: CalculadoraKW         │  │
│  │  - Tabla: Aparatos                    │  │
│  │  - Tabla: UsoAparatos                 │  │
│  └───────────────────────────────────────┘  │
└─────────────────────────────────────────────┘
```

## 🔐 Seguridad para Producción

Si quieres desplegar en servidor remoto:

1. **Cambia la contraseña de BD** en `.env`
2. **Usa un reverse proxy** (Nginx, Caddy) con HTTPS
3. **Restringe puertos de BD** (no exponer 1433 al público)
4. **Usa variables de entorno** en lugar de `.env` en producción

Ejemplo para Azure:
```bash
docker-compose -f docker-compose.yml up -d
```

## 📞 Soporte

¿Problemas? Verifica:
1. Docker Desktop está corriendo
2. Puertos 5001, 8081, 1433 están libres
3. Revisión de logs: `docker-compose logs`

## 📄 Licencia

Este proyecto es de uso privado/educativo.

---

**Versión**: 1.0  
**Última actualización**: Diciembre 2025
