
const CanvasDisplay = {};

CanvasDisplay.VERSION = "0.0.1";

CanvasDisplay.Load = function (canvas, ctx, BoxFrame) {

    console.log("loaded");
    console.log(canvas);
    //this = CanvasDisplay
    this.canvas = canvas;
    this.ctx = ctx;
    this.BoxFrame = BoxFrame;

    CanvasDisplay.SETTINGS();

    this.PointList = [];
    this.PolygoneList = [];
    //
    this.PolygonID = 0;
    this.PointID = 0;
    //Math


    //const In = window.innerWidth / 2;
    this.canvas.addEventListener("mousedown", function (e) { CanvasDisplay.MouseClick(e) })
    //canvas.addEventListener("mouseup", ff)
    canvas.addEventListener("mousemove", function (e) { CanvasDisplay.MouseMove(e) })

}

CanvasDisplay.SETTINGS = function () {
    this.OGINAL_WIDTH = 1280;
    this.OGINAL_HEIGHT = 720;
    this.TILE_SIZE = 64;
    this.CANVAS_WIDTH = 1280;
    this.CANVAS_HEIGHT = 720;
    this.CURREMT_WIDTH = 1280;
    this.CURREMT_HEIGHT = 720;
    this.canvas.width = this.OGINAL_WIDTH;
    this.canvas.height = this.OGINAL_HEIGHT;
    this.DotHit = false;

}
CanvasDisplay.Resize = function () {
    this.CanvasRect = this.canvas.getBoundingClientRect();
    this.WIDTH = this.BoxFrame.offsetWidth - this.BoxFrame.offsetWidth / 4;
    this.HEIGHT = this.WIDTH / 1.5;

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
CanvasDisplay.NewPolygone = function (PointList, x, y) {
    this.PolygoneList.push({ ID: this.PolygonID++, list: PointList, x: x, y: y });
}
CanvasDisplay.NewPoint = function (x, y, m) {
    this.PointID = this.PointList.length;
    this.PointList.push([Math.floor(x), Math.floor(y), m, this.PolygonID, this.PointID++]);
}
CanvasDisplay.MovePoint = function (ID, XX, YY, m, p) {
    this.PointList[ID] = [XX, YY, m, p, ID];
}
CanvasDisplay.MouseMove = function (evt) {
    this.MOUSE = { e: evt, x: (evt.clientX - this.CanvasRect.left) * this.OGINAL_WIDTH / this.WIDTH, y: (evt.clientY - this.CanvasRect.top) * this.OGINAL_HEIGHT / this.HEIGHT }
    this.HitDot(Math.floor(this.MOUSE.x), Math.floor(this.MOUSE.y));

    this.DrawView();
}



CanvasDisplay.MouseClick = function (evt) {

    //console.log((evt.clientX - rect.left)* CanvasDisplay.OGINAL_WIDTH/CanvasDisplay.WIDTH , "Resize");
    this.MOUSE = { e: evt, x: (evt.clientX - this.CanvasRect.left) * this.OGINAL_WIDTH / this.WIDTH, y: (evt.clientY - this.CanvasRect.top) * this.OGINAL_HEIGHT / this.HEIGHT }


    this.HitDot(Math.floor(this.MOUSE.x), Math.floor(this.MOUSE.y));

    if (this.DotHit == false) {
        this.NewPoint(this.MOUSE.x, this.MOUSE.y, 0);
        this.Save();
    }
    this.DrawView();
}
CanvasDisplay.Draw = function () {
    console.log("Draw");
    this.ctx.beginPath();
    this.ctx.rect(20, 20, 150, 100);
    this.ctx.stroke();
}
CanvasDisplay.DrawView = function (ViewID) {
    this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
    let len = this.PolygoneList.length;
    for (var i = 0; i < len; i++) {
        this.DrawPolygone(this.PolygoneList[i].list, this.PolygoneList[i].x, this.PolygoneList[i].y, '#0a0', 4);
    }
    len = this.PointList.length;
    for (var i = 0; i < len; i++) {
        this.DrawDot(this.PointList[i][0], this.PointList[i][1], this.PointList[i][2]);
    }
}
CanvasDisplay.DrawPolygone = function (Points, x, y, col, m) {
    this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
    this.ctx.beginPath();
    let len = Points.length;
    this.ctx.fillStyle = col;
    this.ctx.lineWidth = m;
    //console.log(x, x, Points,"DrawPolygone");
    this.ctx.moveTo(Points[0][0] + x, Points[0][1] + y);
    for (var i = 0; i < len - 1; i++) {
        this.ctx.lineTo(Points[i + 1][0] + x, Points[i + 1][1] + y);
    }
    this.ctx.closePath();
    this.ctx.fill();
    this.ctx.stroke();
    for (var i = 0; i < len; i++) {
        this.DrawDot(Points[i][0] + x, Points[i][1] + y, Points[i][2]);
    }
}
CanvasDisplay.DrawDot = function (x, y, cf) {

    let radius = 8;
    //let pos = getMousePos(evt);
    //let insidep = this.Mathstuff.exports([x, y], this.PointList, 0)
    //- CANVAS_HEIGHT  - CANVAS_WIDTH/2  
    //console.log(this.DotHit, cf,"DrawDot");
    if (cf == 1) {
        this.ctx.fillStyle = "#FF0000";
    } else {
        this.ctx.fillStyle = "#0000FF";
    }

    this.ctx.beginPath();
    this.ctx.lineWidth = 2;
    this.ctx.strokeStyle = '#003300';
    this.ctx.arc(x, y, radius, 0, 2 * Math.PI, false);
    this.ctx.fill();

    this.ctx.stroke();
}



CanvasDisplay.Save = function () {

    let pstring = "0:0";

    for (var i = 0; i < this.PointList.length; i++) {
        pstring += "," + this.PointList[i][0].toString(16).toUpperCase() + ":" + this.PointList[i][1].toString(16).toUpperCase();
    }
    this.SaveTag.innerHTML = pstring;
    this.SaveTag.value = pstring;
}
CanvasDisplay.LoadPoints = function () {
    var data = this.SaveTag.value.split(",");
    let PPList = [];
    if (data.length > 2) {
        for (var i = 1; i < data.length; i++) {
            let txtData = data[i].split(":");
            let readX = parseInt(txtData[0], 16);
            let readY = parseInt(txtData[1], 16);
            PPList.push([readX, readY, 0, 0]);
        }
    }
    let cods = data[0].split(":");
    this.NewPolygone(PPList, parseInt(cods[0]), parseInt(cods[1]));
    console.log(PPList, this.PolygoneList, "load");
    this.DrawView(0);
}
CanvasDisplay.ClearView = function () {
    this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
    this.PointList = [];
    this.PolygoneList = [];
}
CanvasDisplay.Debug = function () {
    this.SaveTag.value = "0:0,FE:1B5,115:224,1A7:255,23B:252,2A1:214,2DD:1A9,2EC:12A,2C1:9E,247:64,1C1:75,202:D4,26F:EE,28B:148,25D:1B0,1FD:1E8,195:1C3,205:1AC,22F:166,218:11E,1C7:118,1EC:145,1D5:17A,196:18D,151:16D,113:169"
    for (var i = 0; i < 4; i++) {
        this.NewPoint(0, 0, 0);
    }
    this.LoadPoints();
}



CanvasDisplay.HitDot = function (px, py) {

    this.DotHit = false;
    if (this.PolygoneList.length > 0) {
        //let PP = this.Mathstuff.Intersects(this.PointList[0][0], this.PointList[0][1], this.PointList[1][0],
        //  this.PointList[1][1], this.PointList[2][0], this.PointList[2][1], this.PointList[3][0], this.PointList[3][1]);
        //let PP = this.Mathstuff.getIntersection(this.PointList[0][0], this.PointList[0][1], this.PointList[1][0],
        //   this.PointList[1][1], this.PointList[2][0], this.PointList[2][1], px, py);
        //let lineA = this.Mathstuff.Line(0, 0, px, py);
        //let lineB = this.Mathstuff.Line(this.PointList[0][0], this.PointList[0][1], this.PointList[1][0], this.PointList[1][1]);
        // let PP = this.Mathstuff.GetLineIntersection(lineA, lineB);
        //   this.PointList[1][1], this.PointList[2][0], this.PointList[2][1], px, py);
        let PP = this.Mathstuff.Polygonetest(this.PolygoneList[0].list, px, py);

        for (var i = 0; i < this.PointList.length && i < PP.length; i++) {
            this.MovePoint(i, PP[i][0], PP[i][1], 1);
        }
        // this.PointList.push([Math.floor(PP.x), Math.floor(PP.y), 1]);


        //  console.log(lineA, lineB, this.PointList, "end");
    }
    //console.log(this.Mathstuff.polygonetest(this.PointList, px, py));

    let len = this.PointList.length;

    for (var i = 0; i < len - 1; i++) {
        let x = this.PointList[i][0];
        let y = this.PointList[i][1];
        if (CanvasDisplay.Mathstuff.GetDistance(x, y, px, py) < 8) {
            this.PointList[i][2] = 1;
            this.DotHit = true;
        } else {
            this.PointList[i][2] = 0;
        }
    }
}

CanvasDisplay.Mathstuff = {};

CanvasDisplay.Mathstuff.Line = function (XX, YY, EX, EY) {
    function Line(XX, YY, EX, EY) {
        this.Sx = XX;
        this.Sy = YY;
        this.Ex = EX;
        this.Ey = EY;
        this.w = this.W();
        this.h = this.H();
    }
    Line.prototype.toString = function () {
        return "StartX: " + this.Sx + ", StartY: " + this.Sy + ", EndX: " + this.Ex + ", EndY: " + this.Ey;
    };
    Line.prototype.toArray = function () {
        return [this.Sx, this.Sy, this.Ex, this.Ey];
    };
    Line.prototype.W = function () {
        //Return Whight
        return this.Ex - this.Sx;
    }
    Line.prototype.H = function () {
        //Return Whight
        return this.Ey - this.Sy;
    }
    Line.prototype.lengt = function () {
        //Return Whight
        return Math.sqrt(this.W() * this.W() + this.H() * this.H());
    }

    return new Line(XX, YY, EX, EY);
};
CanvasDisplay.Mathstuff.Intersects = function (a, b, c, d, p, q, r, s) {
    var det, gamma, lambda;
    det = (c - a) * (s - q) - (r - p) * (d - b);
    if (det === 0) {
        return false;
    } else {
        lambda = ((s - q) * (r - a) + (p - r) * (s - b)) / det;
        gamma = ((b - d) * (r - a) + (c - a) * (s - b)) / det;
        return (0 < lambda && lambda < 1) && (0 < gamma && gamma < 1);
    }
};
CanvasDisplay.Mathstuff.GetLineIntersection = function (lineA, lineB) {
    //, line1EndX, line1EndY, line2StartX, line2StartY, line2EndX, line2EndY
    // if the lines intersect, the result contains the x and y of the intersection (treating the lines as infinite) and booleans for whether line segment 1 or line segment 2 contain the point
    let denominator, a, b, numerator1, numerator2, result = {
        x: null,
        y: null,
        IsOnLineA: false,
        IsOnLineB: false
    };

    denominator = (lineB.h * lineA.w) - (lineB.w * lineA.h);

    if (denominator == 0) {
        return result;
    }
    a = lineA.Sy - lineB.Sy;
    b = lineA.Sx - lineB.Sx;
    numerator1 = (lineB.w * a) - (lineB.h * b);
    numerator2 = (lineA.w * a) - (lineA.h * b);
    a = numerator1 / denominator;
    b = numerator2 / denominator;

    // if we cast these lines infinitely in both directions, they intersect here:
    result.x = lineA.Sx + (a * (lineA.w));
    result.y = lineA.Sy + (a * (lineA.h));
    /*
        // it is worth noting that this should be the same as:
        x = line2StartX + (b * (line2EndX - line2StartX));
        y = line2StartX + (b * (line2EndY - line2StartY));
        */
    // if line1 is a segment and line2 is infinite, they intersect if:
    if (a > 0 && a < 1) {
        result.IsOnLineA = true;
    }
    // if line2 is a segment and line1 is infinite, they intersect if:
    if (b > 0 && b < 1) {
        result.IsOnLineB = true;
    }
    // if line1 and line2 are segments, they intersect if both of the above are true
    return result;
};
CanvasDisplay.Mathstuff.GetDistance = function (xA, yA, xB, yB) {
    let xDiff = xA - xB;
    let yDiff = yA - yB;

    return Math.sqrt(xDiff * xDiff + yDiff * yDiff);
};

CanvasDisplay.Mathstuff.Polygonetest = function (polyPoints, x, y) {

    let lineA = this.Line(0, 0, x, y);
    let lineB = this.Line(polyPoints[polyPoints.length - 1][0], polyPoints[polyPoints.length - 1][1], polyPoints[0][0], polyPoints[0][1]);
    let InterCnt = 0;

    let test = this.GetLineIntersection(lineA, lineB);
    //let boolval = test.IsOnLineB;
    let TestPoints = [];
    //

    for (var i = 0; i < polyPoints.length - 1; i++) {
        // InterCnt++;  if (test.IsOnLineA == true && test.IsOnLineB == true)
        if (test.IsOnLineA && test.IsOnLineB) TestPoints.push([test.x, test.y]);
        //console.log(test, test.IsOnLineA && test.IsOnLineB);
        lineB = this.Line(polyPoints[i][0], polyPoints[i][1], polyPoints[i + 1][0], polyPoints[i + 1][1]);
        test = this.GetLineIntersection(lineA, lineB);
        //boolval = test.IsOnLineB;
        //if polygon point x is lower than click x, th
        //if (test.x < x && boolval) boolval = false;
    }
    if (test.IsOnLineA && test.IsOnLineB) TestPoints.push([test.x, test.y]);
    // console.log(TestPoints, "Polygonetest");
    //N = nuber of intersections, NX max intesections
    //When N is odd you inside
    /* res = { N: 0, NX: TestPoints.length};
     if (TestPoints.length == 0) return res;
     let cntL = 0;
     let cntR = 0;
     for (var i = 0; i < TestPoints.length; i++) {
         if (x < TestPoints.x) cntL++
         if (x >= TestPoints.x) cntR++;
     }
     if (cntL == TestPoints.length || cntR == TestPoints.length) return res;
     res.N = cntL;
     //*/
    return TestPoints
};

window.addEventListener("load", () => {

    let canvas = document.querySelector("#mycanvas");
    let BoxFrame = canvas.parentElement;
    let ctx = canvas.getContext("2d")

    CanvasDisplay.SaveTag = document.querySelector("#drawpointsdisplayData");

    CanvasDisplay.Load(canvas, ctx, BoxFrame);
    CanvasDisplay.Resize();
    //
    // ctx.fillStyle = 'green';
    // ctx.fillRect(0, 0, canvas.width, canvas.height)
});
window.addEventListener("resize", () => {

    CanvasDisplay.Resize();
});
