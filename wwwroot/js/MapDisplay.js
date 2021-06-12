
var HttpClient = function () {
    this.get = function (aUrl, aCallback) {
        var anHttpRequest = new XMLHttpRequest();
        anHttpRequest.onreadystatechange = function () {
            if (anHttpRequest.readyState == 4 && anHttpRequest.status == 200)
                aCallback(anHttpRequest.responseText);
        }

        anHttpRequest.open("POST", aUrl, true);
        anHttpRequest.send();
    }
}


const CanvasDisplay = {};

CanvasDisplay.VERSION = "0.0.1";
CanvasDisplay.Load = function (canvas, ctx, BoxFrame) {
    this.canvas = canvas;
    this.ctx = ctx;

    this.BoxFrame = BoxFrame;
    this.demotext = "demo";
    this.HitEvent = false;

    CanvasDisplay.SETTINGS();

    //const In = window.innerWidth / 2;
    this.canvas.addEventListener("mousedown", function (e) { CanvasDisplay.MouseClick(e) })
    //canvas.addEventListener("mouseup", ff)
    this.canvas.addEventListener("mousemove", function (e) { CanvasDisplay.MouseMove(e) })
}
CanvasDisplay.SETTINGS = function () {
    this.OGINAL_WIDTH = 1120;
    this.OGINAL_HEIGHT = 720;
    this.TILE_SIZE = 64;
    this.CANVAS_WIDTH = 1120;
    this.CANVAS_HEIGHT = 720;
    this.CURREMT_WIDTH = 1120;
    this.CURREMT_HEIGHT = 720;
    this.canvas.width = this.OGINAL_WIDTH;
    this.canvas.height = this.OGINAL_HEIGHT;
    this.DotHit = false;
    this.CanvasRect = this.canvas.getBoundingClientRect();
}


CanvasDisplay.Resize = function () {
    this.CanvasRect = this.canvas.getBoundingClientRect();
    this.WIDTH = this.BoxFrame.offsetWidth - this.BoxFrame.offsetWidth / 4;
   // this.HEIGHT = this.WIDTH / 1.3;

    let ratio = 16 / 9;
    if (this.HEIGHT < this.WIDTH / ratio)
        this.WIDTH = this.HEIGHT * ratio;
    else
        this.HEIGHT = this.WIDTH / ratio;

    //this.ctx.mozImageSmoothingEnabled = false;	//better graphics for pixel art
    //this.ctx.msImageSmoothingEnabled = false;
    //this.ctx.imageSmoothingEnabled = false;

    this.CURREMT_WIDTH = this.OGINAL_WIDTH / this.canvas.width;
    this.CURREMT_HEIGHT = this.canvas.height / this.OGINAL_HEIGHT;

    //console.log(this.CURREMT_WIDTH, this.CURREMT_HEIGHT, "Resize");

    this.canvas.style.width = '' + this.WIDTH + 'px';
    this.canvas.style.height = '' + this.HEIGHT + 'px';




}

CanvasDisplay.MouseMove = function (evt) {
    this.MOUSE = { e: evt, x: (evt.clientX - this.CanvasRect.left), y: (evt.clientY - this.CanvasRect.top) + window.pageYOffset }
    //this.HitDot(Math.floor(this.MOUSE.x), Math.floor(this.MOUSE.y));
    this.HitEvent = false;
    if (this.MOUSE.x > 306 && this.MOUSE.y > 295) {
        if (this.MOUSE.x < 494 && this.MOUSE.y < 457) {
            this.HitEvent = true;
        }
    }
    this.DrawView();
}



CanvasDisplay.MouseClick = function (evt) {
    this.MOUSE = { e: evt, x: (evt.clientX - this.CanvasRect.left), y: (evt.clientY - this.CanvasRect.top) + window.pageYOffset }
    if (this.MOUSE.x > 306 && this.MOUSE.y > 295) {
        if (this.MOUSE.x < 494 && this.MOUSE.y < 457) {
            //alert("du har clicked et event");
            //document.cookie = "username=John Doe"
            //post("index", { element: "osmium" });
            //var xhr = new XMLHttpRequest();
            //xhr.open("POST", "index", true);
            //xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            //xhr.send('param1=val1');
            location.replace("Events/Event?id=1")
            /*sessionStorage.setItem("jsData", "test");
            var xhr = new XMLHttpRequest();
            // we defined the xhr

            xhr.onreadystatechange = function () {
                if (this.readyState != 4) return;

                if (this.status == 200) {
                    var data = JSON.parse(this.responseText);

                    // we get the returned data
                }

                // end of state change: it can be after some time (async)
            };
           
            xhr.open('GET', "index", true);
            xhr.send();
             */
             //
            //location.reload();
        }
    }
}

CanvasDisplay.DrawView = function (ViewID) {
    this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
    this.ctx.beginPath();
   // this.ctx.rect(306, 295, 150, 100);
    if (this.HitEvent) {
        this.ctx.fillStyle = "#006600";
        this.ctx.fillRect(306, 295, 188, 162);
        this.ctx.fillStyle = "#ffffff";
    } else { 
        this.ctx.fillStyle = "#00FF00";
        this.ctx.fillRect(306, 295, 188, 162);
        this.ctx.fillStyle = "#000000";
    }
    this.ctx.font = "20px Arial";
    this.ctx.fillText(this.demotext, 390, 376);

   
   // this.ctx.fillRect(this.MOUSE.x - 10, this.MOUSE.y - 10, 20, 20);
    this.ctx.stroke();
}


window.addEventListener("load", () => {

    let canvas = document.querySelector("#mycanvas");
    let img = document.querySelector("#imgsource");
    
    let BoxFrame = canvas.parentElement;
    let ctx = canvas.getContext("2d")

  
    CanvasDisplay.SaveTag = document.querySelector("#drawpointsdisplayData");

   // var myVar = setInterval(myTimer, 800);
    // document.getElementById("imgsource").innerHTML = "<img src=\"floors/ConfrindMain2.PNG\" alt=\"ConfrindMain\">";


    CanvasDisplay.Load(canvas, ctx, BoxFrame);
    //CanvasDisplay.Resize();
     // function myTimer() {
     //     var d = new Date();
      //    document.getElementById("imgsource").innerHTML = "<img src=\"floors/ConfrindMain2.PNG\" alt=\"ConfrindMain\">";
     // }

});
window.addEventListener("resize", () => {
    CanvasDisplay.CanvasRect = CanvasDisplay.canvas.getBoundingClientRect();
   // CanvasDisplay.Resize();
});

window.post = function (url, data) {
    return fetch(url, { method: "POST", body: JSON.stringify(data) });
}

// ...


/*

image.addEventListener('load', e => {
    ctx.drawImage(image, 33, 71, 104, 124, 21, 20, 87, 104);
});

*/










