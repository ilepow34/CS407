#pragma strict

//this class is designed to store all of the gameobjects into tables so that we can more easily access them.
//I thought of this when I was playing around with some RTS resource tutorials. It's built to be easily modifiable.
//Let me know if anyone has a better system.

var buildings:GameObject[];
//this has not been implement yet, but is a temp object that will point to a table of buildings. Will work once buildings
//are implemented.

var units:GameObject[];
//Thinking about this, we might want to separate units by their different types.
//we also don't have a way to divide teams yet to control who controls which units.
// we could do it somewhere in the game manager, or we could do it in the unit class itself.

function Start () {
	
}

function addToArray(addition:GameObject, array:GameObject[]):GameObject[]{
    var tempArray:GameObject[] = new GameObject[array.length+1];
    for (var i = 0; i < array.length; i++){
        tempArray[i] = array[i];
    }
    tempArray[tempArray.length-1] = addition;
    return tempArray;
}


function addBuildings(source:GameObject){
        buildings = addToArray(source, buildings);
}

    function addUnits(source:GameObject){
        units = addToArray(source, units);
    }
