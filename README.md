# Digital Twin MVC   
Simulación de una línea de producción utilizando ASP.NET Core MVC y patrones de diseño.

Este proyecto representa un **Clon Digital (Digital Twin)** básico para visualizar y controlar el comportamiento de máquinas industriales.  
Permite encender/apagar máquinas, visualizar telemetría (temperatura, presión, RPM), registrar eventos y simular fallas.

---

## Objetivo
Simular el ciclo de vida y funcionamiento de una línea de producción, aplicando **buenas prácticas de arquitectura y patrones de diseño**.

---

## Funcionalidades
- Encender y apagar máquinas
- Cambiar a modo de mantenimiento
- Simular fallos automáticos
- Registrar historial de eventos
- Mostrar telemetría simulada en tiempo real
- Vista tipo **dashboard** para control

---

## Patrones de Diseño Aplicados
| Patrón | Dónde se usa | Descripción |
|------|-------------|-------------|
| **State** | Máquina (`Off`, `Operational`, `Maintenance`, `Error`) | Maneja el ciclo de vida de cada máquina sin condicionales complejos |
| **Observer** | `EventBus` y registros de eventos | Permite notificar y mostrar el historial de cambios |
| **Strategy** | Proyección de sensores (telemetría) | Simula cambios en temperatura, presión y RPM |
| **Prototype + Factory Method** | Creación de máquinas | Permite agregar nuevas máquinas sin duplicar código |
| **Command** | Acciones Start/Stop/Maintenance/Ack | Encapsula acciones para mantener el controlador simple |

---


