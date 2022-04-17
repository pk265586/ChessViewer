﻿const TurnWhite = 0;
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

// convert 0-base integer to board lowercase char
function intToBoardXChar(xBase0) {
    return String.fromCharCode('a'.charCodeAt(0) + xBase0);
}

// convert board char to 0-base integer
function boardXCharToInt(xChar) {
    return xChar.toLowerCase().charCodeAt(0) - 'a'.charCodeAt(0);
}

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

    makeMove() {
        if (this.currentMoveNumber <= 0 || this.currentMoveNumber > this.moves.length)
            return;

        let currentMove = this.moves[this.currentMoveNumber - 1];
        let moveSquares = this.extractMoveSquares(currentMove, this.currentTurn);
        this.makeMoveCore(moveSquares);
    }

    makeMoveCore(moveSquares) {
        if (!moveSquares.from || !moveSquares.to)
            return;

        let movingPiece = this.position[moveSquares.from.y][moveSquares.from.x];
        this.position[moveSquares.from.y][moveSquares.from.x] = 0;
        this.position[moveSquares.to.y][moveSquares.to.x] = movingPiece;

        this.currentTurn = (TurnWhite + TurnBlack) - this.currentTurn;
        if (this.currentTurn === TurnWhite) {
            this.currentMoveNumber++;
        }
    }

    extractMoveSquares(move, turn) {
        let rawSquares = this.extractRawMoveSquares(move, turn);

        return {
            from: this.extractXY(rawSquares.from),
            to: this.extractXY(rawSquares.to)
        };
    }

    extractRawMoveSquares(move, turn) {
        if (turn === TurnWhite) {
            return {
                from: move.WhiteFrom,
                to: move.WhiteTo
            };
        }

        return {
            from: move.BlackFrom,
            to: move.BlackTo
        };
    }

    extractXY(rawSquare) {
        if (!rawSquare || rawSquare.length < 2)
            return null;

        return {
            x: boardXCharToInt(rawSquare[rawSquare.length - 2]),
            y: parseInt(rawSquare[rawSquare.length - 1]) - 1
        };
    }
}

class GameRenderer {
    constructor(game) {
        this.game = game;
    }

    getCellId(x, y) {
        let x_letter = intToBoardXChar(x - 1);
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

class PlayGameEvents {
    constructor(renderer) {
        this.renderer = renderer;
    }

    bindEvents() {
        $("#btn-play").click(() => this.play());
        $("#btn-stop").click(() => this.stop());
        $("#btn-step-forward").click(() => this.stepForward());
        $("#btn-step-backward").click(() => this.stepBackward());
    }

    play() {
    }

    stop() {
    }

    stepForward() {
        this.renderer.game.makeMove();
        this.renderer.renderPosition();
    }

    stepBackward() {
    }
}

//var events;

function doShowGame(moves) {
    let game = new ChessGame(moves);
    let renderer = new GameRenderer(game);
    renderer.renderPosition();
    let events = new PlayGameEvents(renderer);
    events.bindEvents();
}
