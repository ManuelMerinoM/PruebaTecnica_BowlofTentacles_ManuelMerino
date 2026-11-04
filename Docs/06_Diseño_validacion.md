# Diseño y Validación

## 1. Descripción general

El proyecto consiste en una experiencia narrativa de terror en primera persona desarrollada en Unity. El jugador debe recuperar una caja perteneciente a su madre, descubrir la combinación que permite abrirla, recoger una llave de repuesto y dirigirse a la puerta de salida.

La experiencia se apoya en la exploración de un espacio doméstico, la lectura de notas y pequeños acontecimientos sonoros y visuales. No incluye persecuciones ni situaciones de muerte: la tensión se construye mediante el ambiente y la información que el jugador va descubriendo.

## 2. Entorno y distribución

El escenario está formado por tres zonas conectadas:

- **Recibidor y pasillo:** contiene la puerta de salida, que comienza cerrada.
- **Salón:** contiene la caja sobre una mesa, una nota y una silla.
- **Cocina:** contiene la segunda nota, que explica la rutina nocturna de la madre.

Desde el salón se puede acceder tanto a la cocina como al pasillo. La distribución permite recorrer las tres zonas sin necesidad de utilizar otras puertas o elementos interactivos ajenos a la experiencia.

## 3. Desarrollo de la experiencia


### 3.1. Llegada

El jugador aparece dentro de la vivienda, mirando hacia el salón.

Al comenzar:

- La puerta de entrada se cierra acompañada de un sonido.
- Se muestra el mensaje: «He venido a recoger la caja de mamá. Después entregaré las llaves».
- La puerta de salida permanece bloqueada.

Si el jugador intenta salir antes de conseguir la llave, aparece el mensaje:

> La llave de repuesto está en su caja.

La caja se encuentra en un lugar visible del salón para orientar la exploración inicial.

### 3.2. Inspección de la caja

La caja se puede inspeccionar y presenta tres botones con símbolos.

Al examinarla se muestra el mensaje:

> Mamá guardaba aquí la llave de repuesto.

Junto a la caja hay una nota que introduce el acertijo:

> Para abrirla, recuerda mis noches: primero lo que preparaba en la cocina, después lo que escuchaba y, por último, la luz que dejaba encendida.

Esta nota indica que la solución está relacionada con la rutina nocturna de la madre.

### 3.3. Descubrimiento de la pista

En la cocina se encuentra la segunda nota:

> Cada noche preparaba una taza de té. Después escuchaba las noticias en la radio del salón. Antes de acostarme dejaba encendida la lámpara del pasillo.

Al cerrar esta nota por primera vez se activa un evento ambiental:

1. Se produce un silencio de aproximadamente dos segundos.
2. Se escuchan tres pasos procedentes del pasillo.
3. La silla del salón cambia discretamente de posición mientras el jugador no la está observando.

El evento se reproduce una sola vez. Su objetivo es generar inquietud y hacer que el jugador preste atención al entorno al regresar al salón.

### 3.4. Resolución del acertijo

El jugador debe relacionar la información de las dos notas y regresar a la caja.

La combinación correcta, expresada mediante los símbolos, es:

1. Taza.
2. Radio.
3. Lámpara.

La solución no utiliza números ni códigos aleatorios. Los tres botones representan elementos concretos de la rutina descrita en la nota de la cocina.

El jugador puede volver a leer ambas notas antes o después de realizar un intento.

Cuando introduce la combinación correcta, la caja se abre y deja visible la llave de repuesto. La llave no se recoge automáticamente: el jugador debe interactuar con ella para incorporarla al inventario.


### 3.5. Salida y desenlace

Una vez recogida la llave, el jugador puede interactuar con la puerta de salida.

Al hacerlo comienza la secuencia final:

1. La puerta se abre.
2. Tras una breve pausa, se escucha o aparece el mensaje: «No te vayas todavía».
3. La puerta se cierra.
4. La imagen se funde a negro.
5. Aparece el texto «Fin».

Durante el desenlace se bloquean las interacciones del jugador. Al terminar se ofrecen las opciones de reiniciar la experiencia o salir del juego.


## 4. Sistemas implementados

La experiencia utiliza los siguientes sistemas principales:

- **Movimiento del jugador:** desplazamiento en primera persona y control de cámara.
- **Interacción:** detección de objetos interactuables y presentación de indicaciones.
- **Inspección de objetos:** permite examinar elementos y leer las notas.
- **Inventario:** gestiona la recogida y el uso de la llave de repuesto.
- **Caja con combinación:** comprueba la secuencia de tres símbolos y reproduce la apertura cuando la solución es correcta.
- **Mensajes en pantalla:** muestra información narrativa y mensajes de interacción.
- **Eventos ambientales:** reproduce los pasos y modifica la posición de la silla al cerrar por primera vez la nota de la cocina.
- **Secuencia final:** controla la animación de la puerta, el mensaje final, el fundido a negro y las opciones posteriores.

## 5. Recursos externos

Los recursos externos utilizados deben quedar identificados para mantener la trazabilidad de sus licencias y autorías.

### Alchemist House

- Recurso: [Alchemist House – Unity Asset Store](https://assetstore.unity.com/packages/3d/environments/alchemist-house-112442#asset_quality)
- Uso: recursos del entorno 3D.
- Licencia: Standard Unity Asset Store EULA

### Vintage Retro Radio

- Recurso: [Vintage Retro Radio – Sketchfab](https://sketchfab.com/3d-models/vintage-retro-radio-26fb0de1015d4570bac28cd8b1fb0a62)
- Autor: Pavlo_Holubov.
- Licencia indicada: Creative Commons Attribution 4.0 (CC BY 4.0).
- Atribución:

  «VINTAGE RETRO RADIO», de Pavlo_Holubov, bajo licencia CC BY 4.0.  
  https://skfb.ly/6VvNF


### Sonidos de Freesound

Se han utilizado sonidos procedentes de Freesound.org.

### Uso de IA

Se ha usado Claude para hacer unas plantillas de los documentos md ya que no había usado este formato con anterioridad.

También se ha usado para resolución de errores en el código.


