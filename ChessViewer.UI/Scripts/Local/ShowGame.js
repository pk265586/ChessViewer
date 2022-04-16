const TurnWhite = 0;
const TurnBlack = 1;
const Wking = 1;
const Wqueen = 2;
const Wrook = 3;
const Wbishop = 4;
const Wknight = 5;
const Wpawn = 6;
const Bking = 7;
const Bqueen = 8;
const Brook = 9;
const Bbishop = 10;
const Bknight = 11;
const Bpawn = 12;

class ChessGame {
    constructor(moves) {
        this.moves = moves;
        this.currentMoveNumber = 1;
        this.currentTurn = TurnWhite;
        this.position = [
            [Wrook, Wknight, Wbishop, Wqueen, Wking, Wbishop, Wknight, Wrook],
            [Wpawn, Wpawn, Wpawn, Wpawn, Wpawn, Wpawn, Wpawn, Wpawn],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0],
            [Bpawn, Bpawn, Bpawn, Bpawn, Bpawn, Bpawn, Bpawn, Bpawn],
            [Brook, Bknight, Bbishop, Bqueen, Bking, Bbishop, Bknight, Brook],
        ];
    }
}


class GameRenderer
{
    constructor(game) {
        this.game = game;
    }

    getCellId(x, y) {
        let x_letter = String.fromCharCode('a'.charCodeAt(0) + x - 1);
        return "square-" + x_letter + y;
    }

    getImageName(piece) { 
        switch (piece) {
            case Wking: return "WKing.png";
            case Wqueen: return "Wqueen.png";
            case Wrook: return "WRook.png";
            case Wbishop: return "Wbishop.png";
            case Wknight: return "Wknight.png";
            case Wpawn: return "Wpawn.png";
            case Bking: return "BKing.png";
            case Bqueen: return "Bqueen.png";
            case Brook: return "Brook.png";
            case Bbishop: return "Bbishop.png";
            case Bknight: return "Bknight.png";
            case Bpawn: return "Bpawn.png";
        }
    }

    getInnerHtml(piece) { 
        if (piece == 0)
            return "";
        return "<img align='center' width='100%' height='100%' src='/Content/Images/" + this.getImageName(piece) + "' />";
    }

    renderPosition() {
        for (let y = 1; y <= 8; y++) {
            for (let x = 1; x <= 8; x++) {
                let cellId = this.getCellId(x, y);
                let cell = $("#" + cellId);
                let innerHtml = this.getInnerHtml(this.game.position[y - 1][x - 1]);
                cell.html(innerHtml)
            }
        }
    }
}

function doShowGame(moves) {
    let game = new ChessGame(moves);
    let renderer = new GameRenderer(game);
    renderer.renderPosition();
}
