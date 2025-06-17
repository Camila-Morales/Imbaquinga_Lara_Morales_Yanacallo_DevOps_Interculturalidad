✈️ Viajecitos SA - Sistema de Compra de Vuelos

# Descripción del Proyecto
Este proyecto es un sistema completo de gestión y compra de vuelos desarrollado para la empresa ficticia Viajecitos SA. Permite a los usuarios buscar vuelos, seleccionar múltiples vuelos, elegir la cantidad de boletos, y realizar compras con dos formas de pago:

- Efectivo: con descuento automático del 5% sobre el total.

- Crédito: genera una tabla de amortización con pagos mensuales fijos usando el método francés.

- El sistema también cuenta con una interfaz de administración para visualizar todas las compras realizadas.

# Arquitectura del Sistema

El proyecto sigue una arquitectura cliente-servidor basada en APIs RESTful:

# 🔧 Backend - Servidor Spring Boot
El backend es una aplicación Java desarrollada con Spring Boot, que expone servicios web REST para:

Autenticación y registro de usuarios

Búsqueda de vuelos

Registro de compras con múltiples vuelos

Reporte de factura

La lógica de negocio está implementada usando el patrón MVC (Model-View-Controller).

# 🌐 Frontend - Cliente Web en React

El cliente web está desarrollado con React.js, que permite a los usuarios:

Registrarse e iniciar sesión

Buscar vuelos disponibles

Agregar varios vuelos

Ver factura

La comunicación entre frontend y backend se realiza usando Axios mediante llamadas HTTP.

# Herramientas y Tecnologías Usadas
Backend (Servidor)
Java 17

Spring Boot 3.x

Spring Web / Spring Data JPA

MySQL como base de datos relacional

Maven para gestión de dependencias

Frontend (Cliente)
React 18+

Axios para consumo de servicios REST

React Router para navegación entre vistas

Bootstrap / Tailwind CSS (según preferencia) para estilos

# Base de Datos

MySQL con las siguientes tablas:

usuarios: guarda datos de login y roles

vuelos: información de los vuelos disponibles

compras: resumen de cada compra

compra_vuelos: relación entre compras y vuelos
