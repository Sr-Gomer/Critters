Taller Practico II Scripting - Nicolás Escobar Espinosa - Juan Camilo Calvache - Andrés Muñoz Ramirez

====================================================================================================
[Consideraciones]

Objetos:
	Critter_U
	Skill_U
	SuportSkill_U : Skill_U
	AtackSkill_U : Skill_U
	Player_U
	Judge_U
	GameManager_U
	CritterInfo_U

Relaciones:
	Un [Player_U] contiene 3 [Critter_U],
	Un [Critter_U] contiene 3 [Skill_U] de tipo [AtackSkill_U] o [SuportSkill_U],
	Un [Judge_U] contiene 2 [Player_U],
	Un [GameManager_U] contiene 1 [Judge_U],
	Un [CritterInfo_U] contiene 1 [Judge_U]

====================================================================================================
[Funcionamiento]

1. Se inicializa el juego
2. El primer Critter del jugador y del enemigo se seleccionan automaticamente
3. El Jugador escoge uno de los tres ataques disponibles de su Critter en juego
4. El Critter enemigo escoge su accion automaticamente
5. Se realizan ambas acciones, el orden de ejecucion depende de la velocidad del Critter
6. Si la habilidad es de AtkUp o DefUp, incrementa los Stats... respectivos del Critter.
7. Si la habilidad es de SpdDown, reduce el StatSpeed del Critter Objetivo.
8. Si la habilidad es de ataque, ifringe daño, de acuerdo a la tabla de Afinidades.
9. Se activa la ventana de mensajes donde, se pueden ver las acciones de los Critters y sus efectos.
10. La barra de vida, la informacion del Critter y el indicador de Critters vivos se modifica de ser necesario.
11. El critter en pantalla y las habilidades a escoger cambian en caso de que muera el critter anterior.
12. Cuando un critter muere este se le quita al jugador y se le asigna al otro jugador.
13. Si los tres critters de un jugador mueren se activa un mensaje de victoria en la ventana de mensajes.

====================================================================================================
[Funcionamiento entre codigos]

-El GameManager_U se comunica con el Judge_U y activa el metodo Initialize(), lo cual inicializa al Judge_U asignando los players e identificando el Critter en uso con sus
respectivas habilidades.
-El metodo Action() contenido en el Judge_U el cual se llama cuando se oprime algun boton de habilidad.
-El metodo Action() inicia las 2 corrutinas, PlayerAction() y EnemyAction(), las cuales realizan las habilidades de los critters y las acciones extras como alterar los valores
de de la interfaz y mostrar las acciones realizadas por los Critters en el panel de mensajes.
-Las corrutinas PlayerAction() y EnemyAction() llaman al metodo ChangeCritter(), que se encarga de mandar los critters muertos a la lista de critters del otro Player.
Tambien dichas corrutinas se encargan de comunicarse con el CritterInfo_U respectivo para alterar los datos de el Critter en uso cuando este muere y alterar el indicador de
critters vivos.
-El Judge_U se comunica con el Critter_U para que este le de un mensaje que debe mostrar en el panel de mensajes, referente a alguna accion realizada a alguna propiedad del
Critter objetivo, ya sea su HP(TakeDamage()) o su statSpeed, su statDefense o su statAtack(ReceiveBuff()). Estas acciones dependen del tipo de Skill_U que se utilice.

====================================================================================================
[Diseño orientado a Objetos]
-Este diseño se planteo con un patron de Software tipo fachada, donde el usuario se comunica con el Judge_U, quien se encarga de comunicarse con los demas codigos y activar
las acciones de estos.
Todas las desiciones se toman desde el Judge_U, quien se encarga de gestionar la informacion, ademas de alterar las propiedades de los Critters y la interfaz.