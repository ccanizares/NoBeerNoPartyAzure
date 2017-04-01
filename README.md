No beer, no party
=================

No te pierdas la oportunidad de conocer en un taller 8 servicios de Azure en 45 minutos. Te daré todas las piezas (aplicaciones) y conceptos. Se trata de crear tu infrastructura en Azure, conectar las piezas y disfrutar del experimento!

### Necesitas:

-   Visual Studio 2017 con asp.net core  

-   Subscripción Azure (vale la de prueba)

-   NO es necesario saber programar, ni sería viable hacerlo en 45 min! Se trata de descargar las aplicaciones y configurar settings una vez creados servicios en Azure. 

-   4 o 5 cafes en vena para estar a tope que tenemos poco tiempo y muchas piezas que conectar XD. Se puede realizar el taller en grupos de 2 o más personas, es incluso recomendable.
 
### Conocerás:

-   Web Apps
-   Azure Search
-   Stream Analytics
-   Azure Functions
-   Event Hub
-   Service Bus


Escenario
=========

Estamos en visperas de la celebración de la mayor feria de la cerveza en la ciudad de Barcelona es un evento anual al que se prevee que asistan unas 10.000 personas según el sistema de registro. La organización dispone de un recinto con 100 stands, en cada uno pueden conectarse 10 barriles. 
Todos los artesanos y hipsters de la cerveza quieren dar a conocer su marca y sólo les está permitido tener un barril en cada stand. No todos los fabricantes tendrán stock para paliar la sed de tanto cervezero a lo largo de las 24 horas que dura el evento, por tanto si un fabricante renuncia a su plaza en el stand es una oportunidad para que otro pueda llenar ese hueco con su barril y así dar a conocer a más gente su querida cerveza.   

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
-   Puntuar un modelo (1 a 5).

### Fabricantes (Web)

-   Consultar la cantidad de cerveza que queda en sus barriles. 
-   Consultar huecos en stands y enviar solicitudes (las solicitudes se aprueban o deniegan automáticamente según un algoritmo avanzado que se llama tonto el último).
 
### Organización (Cajero)

-   Almacenar todas las transacciones para explotar esa información en un futuro mediante predicciones y analisis. 


Nuestra propuesta
-----------------

###  

![](https://github.com/ccanizares/NoBeerNoParty/raw/master/assets/nobeernoparty.png)

### Azure Search

Permitirá a la web buscar modelos de cerveza entre los stands y tener información precisa en todo momento de los modelos mejor valorados, cantidad aproximada restante de cada modelo en los stands, etc. 


### Web App

Conoceremos el servicio de aplicaciones escalables en la nube de Azure publicando la aplicación web desde Visual Studio.
 

### Event Hub

Los cajeros enviarán información sobre cada compra, este servicio permite la ingesta de información codificada como stream. 

[+ info](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-standard-getstarted-send)
 

### Stream Analytics 

Estará suscrito a los eventos de Event Hub y será el encagado de procesar y tranformar la información. Una parte la almacenerá en documentDb y otra la dejará en una cola. 

 
### Service Bus

Soporta colas de mensajes, recibirá mensajes por parte del servicio de Stream Analytics (compras) y también recibe mensajes directamente desde la aplicación web (valoraciones)

 
### Azure Functions

Este servicio tiene como objetivo procesar los mensajes de service bus, y actualizar los datos del índice de búsqueda del servicio Azure Search. 

 
### DocumentDB 

Será donde almacenemos toda aquella información que quiere disponer la organización al finalizar el evento para su futuro analisis. 

 
Go!
---

### Crear recursos en azure

1.  Grupo de recursos: Agrupa todos los recursos en un grupo de manera que será más cómodo luego gestionarlos.

2.  Web App: Alojaremos aquí la web que nos facilitan.

3.  Azure Search: De momento no configuramos ningún indice, lo haremos más adelante vía Rest Api.

4.  Document Db: Creamos una base de datos vacía. 

5.  Event Hub: Creamos un topic UserRate y otro BeerShop. 

6.  Stream Analytics: Conectaremos como input el Event Hub creado en el paso anterior y como ouputs uno a documentDb y otro a AzureSearch. Crearemos las transformaciones de modelo necesarias. 

 
### Conectando piezas

1.  Descarga la aplicación de Consola (simulación de la aplicación cajero) y añade los datos de conexión a Event Hub y Azure Search.

2.  Ejecuta la aplicación y pulsa opción 1. Esto generará los indices necesarios en Azure Search.

3.  Ejecuta la aplicación ahora pulsando 2. Esto simulará el tráfico de compras en los cajeros enviando información a nuestro sistema.   

4.  Descarga la aplicación web y añade las cadenas de conexión necesarias para comunicarse con Azure Search y Event Hub. 

5.  Despliega la web (botón derecho y publicar).

 
### Explotando el sistema

1.  Accede a la web que hemos publicado como usuario y usa la funcionalidad de búsqueda.

2.  Como usuario valora una cerveza.

3.  Ahora accede como fabricante y consulta el estado de tus barriles.

4.  Como fabricante busca un hueco vacio y solicita colocar tu barril. 

5.  Conéctate al portal de Azure y comprueba que los datos del evento se están almacenando en Document Db. 
