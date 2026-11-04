Guía para añadir contenido

## 1. Introducción

Esta guía explica cómo ampliar el contenido del juego mediante los sistemas que ya existen en el proyecto.

Se utilizan como ejemplos los objetos inspeccionables, las notas, la caja de códigos, la llave de repuesto y los eventos narrativos.

Los procedimientos distinguen entre:
- **Configuración desde el Inspector:** no requiere modificar código.
- **Modificación mediante código:** requiere crear o editar scripts C#.

## 2. Crear y configurar un objeto interactuable

El sistema de interacción utiliza la interfaz `IInteractable` y el componente `InspectableItem`.

Actualmente existe un prefab de nota que utiliza `InspectableItem`.

### 2.1. Crear un objeto

1. Crear o importar el objeto que se quiere utilizar en la escena.
2. Comprobar que tiene un Collider para que pueda detectarlo el sistema de interacción mediante raycast.
3. Añadir el componente `InspectableItem`.
4. Configurar sus campos desde el Inspector.

### 2.2. Configuración de InspectableItem

Los campos identificados en el Inspector son:

| Campo | Función |
|---|---|
| `Item Name` | Nombre del objeto que se muestra durante la interacción. |
| `Is Note` | Indica si el objeto se trata como una nota. |
| `Note Content` | Texto que se muestra al inspeccionar una nota. |
| `Triggers Living Room Event` | Indica si al cerrar la inspección se activa el evento del salón. |

Para crear una nota:

Usar el prefab de nota con el modelo 3D ya hecho o:

1. Crear el objeto y añadirle `InspectableItem`.
2. Introducir un nombre en `Item Name`.
3. Activar `Is Note`.
4. Escribir el contenido en `Note Content`.
5. Dejar desactivado `Triggers Living Room Event`, salvo que se quiera activar ese acontecimiento al cerrar la nota.

### 2.3. Crear un objeto inspeccionable que no sea una nota

Para crear un objeto que el jugador pueda examinar sin mostrar un texto de nota:

1. Añadir `InspectableItem`.
2. Introducir su nombre en `Item Name`.
3. Desactivar `Is Note`.
4. Dejar vacío `Note Content`.
5. Comprobar que el objeto tiene un Collider.

La interacción y la inspección se gestionan mediante los sistemas existentes, sin necesidad de crear una implementación nueva de `IInteractable`.


## 3. Añadir o modificar una pista

Las pistas se pueden implementar mediante objetos que utilizan `InspectableItem`.

El ejemplo existente es la nota del salón y la de la cocina.

### 3.1. Añadir una nueva pista

**Configuración desde el Inspector:**

1. Crear un objeto para representar la nota.
2. Añadir `InspectableItem`.
3. Introducir un nombre en `Item Name`.
4. Activar `Is Note`.
5. Escribir el texto de la pista en `Note Content`.
6. Colocar la nota en el escenario.
7. Comprobar que el jugador puede detectarla e inspeccionarla.

El contenido se puede modificar posteriormente desde el Inspector sin cambiar el script.

### 3.2. Modificar una pista existente

1. Seleccionar la nota en la escena o su prefab.
2. Localizar el componente `InspectableItem`.
3. Modificar el campo `Note Content`.

### 3.3. Asociar una pista a un evento narrativo

El campo `Room Event` permite asociar una nota al acontecimiento del salón.

Para utilizarlo:

1. Seleccionar la nota correspondiente.
2. Activar `Room Event`.
3. Comprobar que existe un objeto `Events` en la escena con el componente `LivingRoomNoteEvent`.
4. Comprobar que las referencias necesarias del evento están configuradas.

El evento se activa al cerrar la inspección de la nota, no al abrirla.

## 4. Configurar la resolución del puzzle

El puzzle existente es una caja que se abre cuando el jugador introduce una combinación correcta de tres símbolos.

Su lógica se encuentra en `CodeBoxLock`.

### 4.1. Componentes necesarios

La caja utiliza:

- `InspectableItem`, para permitir su inspección.
- `CodeBoxLock`, para gestionar la combinación y la apertura.
- Un Collider para detectar la interacción.
- Un Animator para la animación de apertura.
- Un objeto que representa el contenido de la caja.

La interfaz utiliza botones de imagen, espacios para mostrar los símbolos introducidos y un botón para borrar el último símbolo.

### 4.2. Configuración desde el Inspector

Seleccionar el objeto de la caja y configurar `CodeBoxLock`.

| Campo | Configuración |
|---|---|
| `Symbol Sprites` | Los tres sprites correspondientes a los símbolos de los botones. |
| `Correct Symbols` | Los identificadores de los símbolos en el orden correcto. |
| `Box Animator` | Animator de la caja. |
| `Open Trigger` | Nombre del parámetro Trigger utilizado para abrirla. |
| `Object Inside` | Objeto que se encuentra dentro de la caja. |
| `Audio Source` | Fuente de audio utilizada para el sonido de error. |
| `Failure Sound` | Clip reproducido cuando el código es incorrecto. |
| `Message Controller` | Referencia al controlador de mensajes. |
| `Message` | Texto que se muestra cuando la combinación es incorrecta. |
| `Inspection Message` | Texto que se muestra cuando se inspecciona la caja. |
| `Interaction Collider` | Collider de interacción de la caja. |


### 4.3. Identificadores de los símbolos

Cada botón tiene un identificador numérico:

- Símbolo 0.
- Símbolo 1.
- Símbolo 2.

El array `Correct Symbols` define la secuencia que debe introducir el jugador.

Por ejemplo:

```csharp
correctSymbols = new int[] { 2, 0, 1 };
```

En este ejemplo, la combinación correcta es:

1. Símbolo 2.
2. Símbolo 0.
3. Símbolo 1.

Los identificadores deben coincidir con los asignados a los botones de la interfaz.

### 4.4. Comportamiento de la resolución

Cuando el jugador completa la combinación:

- Si es incorrecta, se muestra el mensaje de error, se reproduce el sonido correspondiente y se vacían los espacios de entrada.
- Si es correcta, la caja se abre y se desactiva su Collider de interacción.

El objeto contenido en la caja es la llave de repuesto, que posteriormente se recoge mediante `KeyPickup`.

### 4.5. Qué se puede modificar sin programar

Desde el Inspector se pueden cambiar:
- La combinación correcta.
- Los sprites de los símbolos.
- El sonido de error.
- El mensaje de error.
- El mensaje de inspección.
- La animación y el objeto contenido.

No es necesario modificar el código para cambiar únicamente estos valores, siempre que se mantenga el funcionamiento actual del sistema.

## 5. Activar eventos de progresión

Los eventos narrativos se encuentran agrupados en un objeto vacío llamado `Events`.

Los scripts de eventos se colocan en ese objeto.

### 5.1. Evento de la cocina

El ejemplo existente es `NoteEvent`.

Este evento se activa al cerrar la inspección de una nota que tenga habilitado `Room Event`.

Su secuencia es:

1. La silla comienza a desplazarse hacia una posición y rotación configuradas.
2. Se produce un intervalo de silencio.
3. Se reproducen tres sonidos de pasos procedentes del pasillo.

### 5.2. Configuración del evento

El componente `LivingRoomNoteEvent` utiliza los siguientes campos:

| Campo | Función |
|---|---|
| `Footsteps Source` | AudioSource que reproduce los pasos. |
| `Footstep Sound` | Clip de sonido de los pasos. |
| `Silence Duration` | Tiempo de silencio antes de los pasos. |
| `Step Interval` | Tiempo entre los pasos. |
| `Chair` | Transform de la silla que se mueve. |
| `Chair Target` | Transform que indica la posición y rotación de destino. |
| `Chair Move Duration` | Duración del movimiento de la silla. |

Para configurar el evento:

1. Seleccionar el objeto `Events`.
2. Localizar `LivingRoomNoteEvent`.
3. Asignar el AudioSource y el clip de pasos.
4. Asignar la silla.
5. Crear un objeto vacío que represente la posición final de la silla y asignarlo a `Chair Target`.
6. Ajustar los tiempos desde el Inspector.
7. Activar `Triggers Living Room Event` en la nota que debe iniciar el evento.

El evento está preparado para ejecutarse una sola vez.

### 5.3. Eventos de progresión

La progresión utiliza componentes como `PlayerInventory`, `KeyPickup` y `DoorInteractable`.

El ejemplo existente consiste en recoger una llave de repuesto y utilizarla para desbloquear una puerta.

La recogida se realiza mediante `KeyPickup`, que actualiza el inventario del jugador. La puerta consulta si el jugador tiene la llave antes de permitir su apertura.

## 6. Añadir un segundo puzzle

Existen dos alternativas para incorporar un segundo puzzle.

### Alternativa A: reutilizar CodeBoxLock

Esta opción permite crear otra caja con una combinación diferente sin desarrollar un sistema nuevo.

**Configuración desde el Inspector:**

1. Crear un segundo objeto que represente la caja.
2. Añadir `InspectableItem`.
3. Añadir `CodeBoxLock`.
4. Configurar los sprites de sus tres símbolos.
5. Introducir una combinación distinta en `Correct Symbols`.
6. Asignar el Animator y el Trigger de apertura.
7. Asignar el objeto que se encuentra dentro.
8. Configurar los sonidos y mensajes.
9. Asignar el Collider de interacción de esa caja.
10. Comprobar que la interfaz y los botones pueden utilizarse correctamente con esta segunda instancia.

La segunda caja puede contener otro objeto y tener una combinación diferente.

Esta alternativa reutiliza la lógica ya existente y requiere principalmente configuración desde el Inspector.

**Ventajas:**
- Menos código nuevo.
- Reutilización del sistema existente.
- Configuración rápida de combinaciones diferentes.
- Menor duplicación de lógica.

### Alternativa B: desarrollar un puzzle nuevo mediante código

Esta opción es adecuada cuando el nuevo puzzle necesita reglas diferentes a las de la caja de símbolos.

Por ejemplo, se podría crear un puzzle de secuencia numérica.

En este caso se puede desarrollar un componente independiente que compruebe la solución.


**Ventajas:**
- Permite crear mecánicas diferentes.
- No obliga a adaptar todas las reglas al funcionamiento de `CodeBoxLock`.
- Facilita desarrollar puzzles con soluciones y comportamientos propios.

**Inconvenientes:**
- Requiere más programación.
- Hay que crear y conectar su interfaz.
- Es necesario probar su integración con la interacción y la progresión.

## 7. Errores habituales

### El objeto no se puede inspeccionar

Comprobar:
- Que tiene `InspectableItem`.
- Que dispone de un Collider.
- Que el Collider está habilitado.
- Que el objeto está dentro del alcance de detección del jugador.

### La nota no muestra su contenido

Comprobar:
- Que `Is Note` está activado.
- Que `Note Content` contiene texto.
- Que el objeto tiene correctamente configurado `InspectableItem`.

### El evento del salón no se ejecuta

Comprobar:
- Que `Room Event` está activado en la nota.
- Que existe `NoteEvent` en la escena.
- Que las referencias de silla, destino y audio están asignadas.
- Que el evento se está cerrando mediante el modo de inspección habitual.

### La caja no reconoce los símbolos

Comprobar:
- Que los identificadores de los botones coinciden con los utilizados en `Correct Symbols`.
- Que los tres sprites están asignados.
- Que el número de posiciones de la combinación coincide con la longitud esperada.
- Que los botones tienen configuradas sus acciones `OnClick`.

### La caja no se abre

Comprobar:
- Que el código introducido es correcto.
- Que `Box Animator` está asignado.
- Que el nombre de `Open Trigger` coincide con el parámetro Trigger del Animator.
- Que el Collider de interacción asignado es el correspondiente a la caja.

### La llave no se puede recoger

Comprobar:
- Que el objeto tiene `KeyPickup`.
- Que tiene un Collider habilitado.
- Que la referencia a `PlayerInventory` está asignada.
- Que el Collider de la caja no está bloqueando la detección de la llave.

### La puerta no reconoce la llave

Comprobar:
- Que `DoorInteractable` tiene asignado el mismo `PlayerInventory` que utiliza `KeyPickup`.
- Que la llave se ha recogido correctamente.
- Que la puerta está configurada como bloqueada.

### Un evento de sonido no se escucha

Comprobar:
- Que el AudioSource está asignado.
- Que el AudioClip correspondiente está asignado.
- Que el volumen no está a cero.
- Que el AudioSource está activo.


