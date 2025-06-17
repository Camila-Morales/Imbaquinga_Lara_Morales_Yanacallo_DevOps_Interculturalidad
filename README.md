✈️ Viajecitos SA - Sistema de Compra de Vuelos

Descripción del Proyecto
Este proyecto es un sistema de gestión y compra de vuelos desarrollado para la empresa Viajecitos SA. Permite a los usuarios buscar vuelos, seleccionar múltiples vuelos, elegir la cantidad de boletos.

Arquitectura del Sistema
El proyecto sigue una arquitectura cliente-servidor basada en APIs RESTful:

🔧 Backend - Servidor .NET
El backend es una aplicación desarrollada con .NET, que expone servicios web REST para:

Autenticación y registro de usuarios
Búsqueda de vuelos
Registro de compras con múltiples vuelos
Reporte de factura
La lógica de negocio está implementada usando el patrón MVC (Model-View-Controller).

🌐 Frontend - Cliente Web en React
El cliente web está desarrollado con React.js, que permite a los usuarios:

Registrarse e iniciar sesión
Buscar vuelos disponibles
Agregar varios vuelos
Ver factura
La comunicación entre frontend y backend se realiza usando Axios mediante llamadas HTTP.

Herramientas y Tecnologías Usadas
Backend (Servidor)

.NET 6+
ASP.NET Core para la creación de APIs REST
Entity Framework Core para acceso a datos
SQL Server como base de datos relacional
Frontend (Cliente)

React 18+
Axios para consumo de servicios REST
React Router para navegación entre vistas
Bootstrap / Tailwind CSS (según preferencia) para estilos
Base de Datos
SQL Server con las siguientes tablas:

usuarios: guarda datos de login y roles
vuelos: información de los vuelos disponibles
compras: resumen de cada compra
compra_vuelos: relación entre compras y vuelos
Reflexiones
¿Cómo se resolvieron los conflictos de timezone?
Durante el desarrollo del proyecto, uno de los desafíos fue la diferencia de zonas horarias entre los miembros del equipo. Para resolver este obstáculo, se establecieron franjas horarias comunes previamente acordadas en las que se podían realizar sesiones de trabajo colaborativo y reuniones breves mediante Google Meet.

Fuera de esos bloques sincrónicos, el equipo adoptó una metodología asíncrona basada en tareas, donde cada integrante trabajaba en su propia rama de Git. Esto permitió que el resto del equipo pudiera revisar y fusionar los cambios sin necesidad de interacción en tiempo real. La comunicación continua mediante WhatsApp también resultó clave para resolver rápidamente dudas o bloqueos sin tener que esperar a las reuniones.

Gracias a esta combinación de estrategias técnicas y organizativas, se logró una colaboración efectiva entre todos los integrantes, a pesar de la diferencia horaria.

¿Qué pasaría si el equipo en India no tiene acceso al servidor backend o base de datos?
En caso de que el equipo en India no tenga acceso directo al servidor .NET, a la base de datos SQL Server local o al entorno de desarrollo usado por el equipo original, se tomarían medidas para asegurar la portabilidad y autonomía del desarrollo remoto. Entre las soluciones aplicadas o consideradas están:

Uso de un repositorio Git centralizado, desde el cual se puede clonar tanto el frontend (React) como el backend (.NET) con las configuraciones necesarias.
Inclusión de un archivo de configuración bien documentado para que cada desarrollador pueda configurar su propia base de datos SQL Server local, con los mismos esquemas y datos iniciales del entorno original.
Exportación de los scripts SQL necesarios para crear las tablas (usuarios, compras, amortizaciones, etc.) y poblarlas con datos básicos de prueba.
Documentación paso a paso para levantar el backend y el cliente en entornos independientes, incluso sin conexión directa al entorno principal del equipo.
Reflexión final
El desarrollo de este sistema representó un proceso colaborativo en el que aplicamos principios de integración y entrega continua, alineados con la cultura DevOps. Desde el inicio, organizamos nuestras tareas mediante ramas en Git, estableciendo flujos de trabajo claros para facilitar los merges y evitar conflictos. Acordamos horarios de trabajo sincrónicos utilizando herramientas como Google Meet y WhatsApp, lo que nos permitió coordinar avances y resolver bloqueos en tiempo real, complementando con trabajo asincrónico bien documentado.

Adoptamos una arquitectura cliente-servidor con tecnologías modernas: el backend fue implementado en .NET, mientras que el frontend se desarrolló en ReactJS, con persistencia de datos en SQL Server. Esta estructura nos permitió desplegar un sistema completo, robusto y flexible.

Desde la perspectiva DevOps, uno de los aportes más relevantes fue la incorporación de pruebas y monitoreo. Para garantizar la calidad del sistema, empleamos Selenium en las pruebas funcionales de la interfaz, automatizando flujos críticos para detectar errores antes del despliegue. Además, integramos Grafana para el monitoreo del rendimiento del servidor, lo que nos permitió visualizar métricas en tiempo real y anticiparnos a posibles cuellos de botella.
