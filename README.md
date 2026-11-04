# PruebaTecnica_BowlofTentacles_ManuelMerino

## 1. Descripción

Este repositorio contiene un proyecto de videojuego desarrollado en Unity para la prueba técnica de Manuel Merino para Bowl of Tentacles.


## 2. Requisitos

- **Unity:** 6000.0.85f1
- **Plataforma de destino:** Windows
- **Lenguaje de programación:** C#
- **Dependencias instaladas:**
  - Input System
  - TextMeshPro
  - Cinemachine

## 3. Instalación y configuración

1. Instalar Unity Hub y Unity Editor versión `6000.0.85f1`.
2. Clonar o descargar el repositorio.
3. Abrir Unity Hub y seleccionar **Add project from disk**.
4. Seleccionar la carpeta del proyecto.
5. Abrir el proyecto utilizando Unity `6000.0.85f1`.
6. Esperar a que Unity termine de importar los recursos y resolver los paquetes.

## 4. Escena de entrada

La escena de entrada es `MainMenu`, configurada como primera escena en la lista de escenas de la build.

Desde esta escena se puede:
- Iniciar el juego mediante el botón **Iniciar**.
- Salir de la aplicación mediante el botón **Salir**.

Al pulsar Iniciar se carga la escena `GameScene`, donde se desarrolla la partida y se ejecutan los eventos iniciales.

## 5. Controles

| Acción | Control |
|---|---|
| Moverse | W, A, S, D |
| Mirar | Ratón |
| Interactuar | E |
| Salir del modo de inspección | Escape |

## 6. Ejecutar el proyecto

1. Abrir el proyecto con Unity `6000.0.85f1`.
2. Abrir la escena `MainMenu`.
3. Pulsar el botón **Play** del Editor.
4. En el menú principal, pulsar **Iniciar** para acceder al juego.


## 7. Generar la build para Windows

1. Abrir el proyecto en Unity.
2. Acceder a **File → Build Profiles**.
3. Seleccionar Windows como plataforma de destino.
4. Comprobar que `MainMenu` y `GameScene` están incluidas en la lista de escenas y que `MainMenu` aparece primero.
5. Seleccionar **Build**.
6. Elegir la carpeta de destino y generar la compilación.
7. Ejecutar el archivo `.exe` generado para comprobar el funcionamiento.

## 8. Ubicación de la build

**Ubicación:** Google Drive

**Enlace de descarga:** https://drive.google.com/file/d/1AW2UoD9AULV7AlGPVZKCRxm85QzuAGQ3/view?usp=sharing

La compilación final esta destinada a Windows.

## 9. Problemas conocidos

Actualmente no se han identificado problemas conocidos.

## 10. Índice de documentación

La documentación adicional se encuentra en la carpeta `docs`:

- `01_Preproduccion`: documentación de preproducción.
- `02_Tareas`: documentación de tareas.
- `03_Memoria_ejecucion`: memoria de ejecución.
