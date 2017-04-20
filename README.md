No beer, no party
=================

No te pierdas la oportunidad de conocer en un taller 8 servicios de Azure en 45 minutos. Te daré todas las piezas (aplicaciones) y conceptos. Se trata de crear tu infrastructura en Azure, conectar las piezas y disfrutar del experimento!

### Necesitas:

-   Visual Studio 2017 con asp.net core  

-   Subscripción Azure (vale la de prueba)

-   NO es necesario saber programar, ni sería viable hacerlo en 45 min! Se trata de descargar las aplicaciones y configurar settings una vez creados servicios en Azure. 

-   Se puede realizar el taller en grupos de 2 o más personas, es incluso recomendable.
 
### Conocerás:

-   Web Apps
-   Azure Search
-   Event Hubs
-   ... 


Escenario
=========

Estamos en visperas de la celebración de la mayor feria de la cerveza en la ciudad de Barcelona es un evento anual al que se prevee que asistan unas 10.000 personas según el sistema de registro. La organización dispone de un recinto con 10 stands, en cada uno pueden conectarse 10 barriles. 

Punto de partida
----------------

-   La organización dispone de una página web que puede ser consumida desde un dispositivo smart phone.
-   Todo asistente al evento debe haberse registrado previamente (también los fabricantes), esta información la tenemos disponible en un conjunto de datos. Al registrarse, el usuario recibe unas claves de acceso.   
-   Disponen de una aplicación en las máquinas registradoras de cada stand que se encarga de registrar cada compra (usuario, modelo cerveza, fecha).


¿Qué nos pide la organización?
------------------------------

Crear la infrastructura necesaria para dar soporte a este evento y conectar sus aplicaciones. Nos facilitan un fichero csv con la información del registro, la aplicación web y la aplicación de cajero para que podamos implementar allí lo que consideremos necesario.
 
### Cervezeros (Web)

-   Buscador de cervezas entre los barriles conectados en stands.  
-   Puntuar un modelo (simulado por la aplicación de consola..)

### Organización (Cajero)

-   Almacenar todas las transacciones para explotar esa información en un futuro.


Nuestra propuesta
-----------------

![](https://github.com/ccanizares/NoBeerNoParty/raw/master/assets/nobeernoparty.png)

### Azure Search
Permitirá a la web buscar modelos de cerveza entre los stands y tener información precisa en todo momento de los modelos mejor valorados, cantidad aproximada restante de cada modelo en los stands, etc. 

### Event Hub
Los cajeros enviarán información sobre cada compra, este servicio permite la ingesta de información codificada como stream. 

### Web App
Servicio de aplicaciones escalables en la nube de Azure publicando la aplicación web desde Visual Studio.

### Azure Functions
Este servicio tiene como objetivo insertar los datos que pasan por event hub en document db.

### DocumentDB 
Será donde almacenemos toda aquella información que quiere disponer la organización al finalizar el evento para su futuro analisis.


Go!
---

### Crear recursos en azure

1.  Grupo de recursos: Agrupa todos los recursos en un grupo de manera que será más cómodo luego gestionarlos.

2.  Web App: Alojaremos aquí la web que nos facilitan.

3.  Azure Search: De momento no configuramos ningún indice, lo haremos más adelante vía Rest Api.

4.  Document Db: Creamos una base de datos vacía. (Si vas justo de tiempo o quieres evitar el coste del recurso, este recurso + Azure functions no son necesarios para la demo de Azure Search que tenemos implementada en la web)

5.  Event Hub: Creamos dos hubs, rating y tickets. 

6.  Azure Function: Creamos dos funciones, ratings y tickets. Como trigger apuntamos a los correspondientes hubs de eventos y outputs a documentdb. El código las functions está disponible aquí. 

 
### Conectando piezas

1.  Descarga la aplicación de Consola (simulación de la aplicación cajero) y añade los datos de conexión a Event Hub, Azure Search y si has optado por crear documentDb.

2.  Descarga la aplicación web y añade las cadenas de conexión necesarias para comunicarse con Azure Search. 

3.  Despliega la web (botón derecho y publicar).

4.  Ejecuta la aplicación y elige una opción.
<ul>
<li>Opción 1: Esto popula la base de datos de document db. (Opcional, no es necesario para la simulación de Azure Search)</li>
<li>Opción 2: Crea el índice de búsqueda en Azure Search.</li>
<li>Opción 3: Popula el índice de búsqueda con los datos iniciales.</li>
<li>Opción 4: Inicia la simulación. (Asegúrate de que todo esté funcionando ok..).</li>
</ul>

 
### Explotando el sistema

1.  Accede a la web que hemos publicado y usa la funcionalidad de búsqueda.

A medida que se generan tickets y valoraciones deberías ir viendo como van cambiiando las cervezas más valoradas y la cantidad que hay en cada dispensador.

2.  Conéctate al portal de Azure y comprueba que los datos del evento se están almacenando en Document Db. 

Ves al explorador de búsqueda de document db y mira los documentos que hemos almacenado en formato json. 