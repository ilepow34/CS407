var selected: boolean = false;
var team = 0;
private var teamColor = [Color.red, Color.blue];

function Start () {
    GetComponent.<Renderer>().material.color = teamColor[team];
}

function Update () {
    if(selected){
        transform.Find("Plane").GetComponent(MeshRenderer).enabled = true;
    }
    else{
        transform.Find("Plane").GetComponent(MeshRenderer).enabled = false;
    }
}
