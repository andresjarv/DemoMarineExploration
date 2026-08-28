# Documentación Técnica: Proyecto de Exploración Submarina

**Autor:** Jorge Andres Vidal Ramirez

## Project Overview
Este proyecto es un videojuego 2D de exploración submarina y supervivencia desarrollado en Unity utilizando el lenguaje C#. El software fue desarrollado con un fuerte enfoque técnico, priorizando la confiabilidad de los sistemas de físicas e iluminación dinámica[cite: 1]. 

El objetivo principal del sistema es descentralizar las mecánicas de juego entre el personaje (submarino) y un ente de luz (dron), creando una experiencia donde el jugador debe gestionar recursos como el oxígeno y resolver puzzles visuales[cite: 1]. El sistema opera como una capa de integración entre los inputs del usuario, el motor de físicas 2D y el entorno construido mediante tilemaps y assets en pixel art[cite: 1].

## System Architecture
La arquitectura está diseñada siguiendo un enfoque modular y distribuido[cite: 1]. El sistema está estructurado en componentes claramente definidos que interactúan entre sí a través del patrón Singleton y eventos de Unity[cite: 1]:

*   **Gestor de Niveles (LevelManager):** Instanciado de forma individual por escena. Evalúa las condiciones de victoria y habilita las puertas de salida.
*   **Capa de Audio (AudioManager):** Un componente centralizado y persistente (`DontDestroyOnLoad`) encargado de procesar los efectos de sonido globales (recolección de ítems, apertura de puertas) sin interrupciones entre escenas.
*   **Control de Estado (PauseManager):** Manipula el motor de tiempo interno (`Time.timeScale`) para detener la física y la lógica global, permitiendo la navegación de menús.
*   **Entidades Físicas:** El jugador y el dron de luz operan bajo `Rigidbody2D`, utilizando materiales de fricción cero para interactuar de forma fluida con las colisiones del entorno.

## System Workflow
El flujo de trabajo del sistema describe la secuencia de eventos que permiten la adquisición de recursos y el control dentro de la plataforma[cite: 1].

1.  **Exploración y Gestión de Oxígeno:** El jugador navega el nivel mientras el sistema reduce constantemente la variable de oxígeno.
2.  **Control de Iluminación:** Mediante el uso del mouse (con una retícula personalizada), el jugador dirige de forma cinemática la esfera de luz hacia áreas oscuras o plantas enemigas (fotofóbicas).
3.  **Recolección Progresiva:** El jugador debe recolectar llaves en cada nivel para enviar una señal de desbloqueo al `LevelManager`. La dificultad y el requerimiento escalan de la siguiente manera:
    *   **Nivel 1:** Requiere la recolección de **1 llave** para abrir la puerta de salida.
    *   **Nivel 2:** Requiere la recolección de **2 llaves** para abrir la puerta de salida.
    *   **Nivel 3:** Requiere la recolección de **3 llaves** para abrir la puerta de salida.
4.  **Transición de Nivel:** Al validar la cantidad de llaves y cruzar el *Trigger* de la puerta habilitada, el sistema invoca una corrutina visual (`SceneFader`) y carga el siguiente escenario.

## Main Functional Components
El juego está compuesto por varios componentes funcionales, cada uno implementado con tecnologías específicas y responsabilidades claramente definidas dentro del sistema[cite: 1].

*   **PlayerController:** Procesa los inputs de movimiento (WASD/Shift) y gestiona un `AudioSource` local para reproducir dinámicamente el ciclo de sonido de nado dependiendo del estado del jugador.
*   **LightDroneController:** Mueve la entidad de luz (`MoveTowards`) interceptando la posición de pantalla del cursor, pero ejecutándose en el `FixedUpdate` para respetar las colisiones del mapa de tiles.
*   **DoorExit / Coleccionables:** Scripts reactivos que escuchan colisiones (`OnTriggerEnter2D`), validan etiquetas de seguridad ("Player"), y se comunican con los Managers antes de destruirse o desvanecerse (Canal Alfa).

## Technology Stack
*   **Motor Gráfico:** Unity Engine.
*   **Lenguaje de Programación:** C#.
*   **Render Pipeline:** Universal Render Pipeline (URP) 2D para soporte de Luces 2D.
*   **Arquitectura de Interfaz:** Unity UI (Canvas, TextMeshPro, FadeGroup).
*   **Control de Versiones:** Git (con exclusiones configuradas para archivos `.meta` del motor).

## Installation Process
Para ejecutar la demostración, el usuario debe cumplir con los siguientes requisitos de software y realizar estos pasos[cite: 1]:

1.  **Descarga:** Obtenga el archivo comprimido en formato `.zip` que contiene la compilación ejecutable para Windows (arquitectura x86_64).
2.  **Descompresión:** Haga clic derecho sobre el archivo `.zip` y seleccione "Extraer todo...". **Nota Crítica:** Extraiga todo el contenido en una misma carpeta local. Es estrictamente necesario que el archivo `.exe` permanezca junto a su subcarpeta respectiva de *Data* (ej. `[NombreJuego]_Data`) para poder iniciar.
3.  **Ejecución:** Acceda a la carpeta donde está almacenado el proyecto y abra el archivo `.exe`[cite: 1]. 
4.  **Navegación del Software:** Una vez dentro, presione el botón "Jugar" para iniciar. Puede pausar la simulación en cualquier momento usando la tecla `Escape` o salir al escritorio de Windows mediante los botones correspondientes en la interfaz gráfica del Menú Principal o el Menú de Pausa.
