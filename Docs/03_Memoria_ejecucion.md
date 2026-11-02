# Memoria de ejecución

## Jornada 1: [28/10/2026]
- **Tiempo aproximado:** 4 horas
- **Tareas realizadas:** Crear repo github y preproducción
- **Decisiones:** Uso de claude para la generación de plantillas de archivos .md para rellenar
- **Comprobaciones:** Se ha subido correctamente el proyecto a GitHub
- **Commits relevantes:** docs: preproducción inicial

## Jornada 1.5: [28/10/2026]
- **Tiempo aproximado:** 2 horas
- **Tareas realizadas:** Crear el graybox del mapa y el movimiento del jugador y de la cámara
- **Decisiones:** Probar el sistema de InputActions de Unity6
- **Problemas y soluciones:** No sabía usar el sistema de InputActions por lo que he seguido un [tutorial de youtube](https://www.youtube.com/watch?v=Lz4_iDzYf9I)
- **Comprobaciones:** Ni la camara ni el personaje transpasan paredes
- **Commits relevantes:** Greybox y Movimiento

## Jornada 2: [29/10/2026]
- **Tiempo aproximado:** 4 horas
- **Tareas realizadas:** Programación de la interfaz de interactuables, modo de inspeccion y notas.
- **Decisiones:** Hacer que los objetos sean inspeccionables por rotación.
- **Problemas y soluciones:** El script no detectaba correctamente la cámara de CineMachine para sacar los raycast, asi que hice una referencia a la cámara original.
- **Comprobaciones:** Los objetos se pueden coger y soltar las veces que sean necesarias y las notas se pueden leer.
- **Commits relevantes:** Objetos Interactuables

## Jornada 3: [30/10/2026]
- **Tiempo aproximado:** 7 Horas
- **Tareas realizadas:** Progamación de la caja y llaves. Gestion de los 3 eventos sin sonido de momento: llegada, coge segunda nota y final.
- **Decisiones:** No volver a poder coger la caja de las llaves vacia, modelar la caja en blender por mi cuenta.
- **Problemas y soluciones:** Al abrir la caja no se podian coger las llaves ya que estaba delante el collider de la caja, he decidido que al soltar la caja se desactive el collider y así coger la llave de adentro.
- **Comprobaciones:** Los eventos se efectuan correctamente y los mensajes se muestran por pantalla.
- **Commits relevantes:** Caja, llaves y eventos.

## Jornada 4: [2/11/2026]
- **Tiempo aproximado:** 6 Horas
- **Tareas realizadas:** Añadido de sonidos y reinicio en el evento final. Cambio de graybox por texturas de [Alchemist House](https://assetstore.unity.com/packages/3d/environments/alchemist-house-112442#asset_quality), sonidos de freesound.org y radio de "VINTAGE RETRO RADIO" (https://skfb.ly/6VvNF) by Pavlo_Holubov is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
- **Decisiones:** He decido usar los assets la casa de alquimista ya que tenía una sensación mas tetrica que sumaba a la ambientación aunque no tenga muchos assets de casa convencional como sofases.
- **Comprobaciones:** Testeos de que todos los sonidos se oyen con claridad, no hay bugs visuales en los objetos de la casa y correcto funcionamiento de la función de reinicio.
- **Commits relevantes:** Reinicio y sonidos. Modelado de la casa.

## Jornada 5: [fecha]
- **Tiempo aproximado:** 
- **Tareas realizadas:** 
- **Decisiones:** 
- **Problemas y soluciones:** 
- **Comprobaciones (build, clon limpio, consola):** 
- **Commits relevantes:** 

---

## Balance final

### Estimación frente a tiempo real

| Bloque | Estimado | Real | Desviación | Motivo |
|---|---|---|---|---|
| A. Preproducción, repo, tablero | 3 h | 4 h| 1h| Tardé mas en escribir de lo que pensaba |
| B. Greybox + movimiento | 3 h | 2 h| -1h| Tarde menos en el greybox de lo que pensaba|
| C. Interacción base + progreso + UI | 4 h | 4 h| 0h| |
| D. Notas, caja, puzzle, llave | 6 h | 4 h| -2h| Tener una interfaz para todos los objetos interactuables facilita la programación |
| E. Tensión | 3 h | 1h | -2h | Los eventos son mas sencillos de lo que pensaba|
| F. Puerta, final, reinicio | 4 h | 2 h| -2h| |
| G. Audio, luz, feedback | 4 h | 2 h | -2h| No eran tantos sonidos para tardar tanto|
| H. Test + correcciones | 4 h | | | |
| I. Documentación | 5 h | | | |
| J. Build y memoria | 4 h | | | |
| **Total** | **40 h** | | | |

### Cambios de alcance y motivos
- 

### Tareas pendientes y cómo continuaría
- 

### Limitaciones conocidas
- La caja al tener solo tres botones es decifrable con la primera nota, pero el código esta contruido de tal manera que sea muy fácil añadir mas botones.

### Qué haría diferente
- 

### Observaciones del test con otra persona
- Qué le costó entender:
- Duración de su partida:
- Cambios que hice a raíz de ello:

