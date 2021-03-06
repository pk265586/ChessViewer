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

// convert 0-base integer to board lowercase char
function intToBoardXChar(xBase0) {
    return String.fromCharCode('a'.charCodeAt(0) + xBase0);
}

// convert board char to 0-base integer
function boardXCharToInt(xChar) {
    return xChar.toLowerCase().charCodeAt(0) - 'a'.charCodeAt(0);
}

function getStartPosition() {
    return [
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

class ChessGame {
    constructor(moves) {
        this.moves = moves;
        this.initGameStart();
    }

    initGameStart() {
        this.currentMoveNumber = 1;
        this.currentTurn = TurnWhite;
        this.position = getStartPosition();
    }

    makeMove() {
        if (!this.isValidCurrentMoveNumber())
            return;

        let currentMoveText = this.moves[this.currentMoveNumber - 1];
        let moveSquares = this.extractMoveSquares(currentMoveText, this.currentTurn);
        this.makeMoveCore(moveSquares);
    }

    backMove() {
        let newTurn = this.getNextTurn();
        let newMoveNumber = newTurn === TurnBlack ? this.currentMoveNumber - 1 : this.currentMoveNumber;
        if (this.isValidMoveNumber(newMoveNumber)) {
            this.setMove(newMoveNumber, newTurn);
        }
    }

    setMove(number, turn) {
        this.initGameStart();
        let safetyCounter = 0;
        while (safetyCounter <= this.moves.length * 2 && !(this.currentMoveNumber === number && this.currentTurn === turn)) {
            this.makeMove();
            safetyCounter++;
        }
    }

    makeMoveCore(moveSquares) {
        if (!moveSquares.from || !moveSquares.to)
            return;

        this.checkCastlingRule(moveSquares);
        this.checkEnPassantRule(moveSquares);
        this.moveOnePiece(moveSquares);

        this.nextMove();
    }

    moveOnePiece(moveSquares) {
        let movingPiece = this.position[moveSquares.from.y][moveSquares.from.x];
        this.position[moveSquares.from.y][moveSquares.from.x] = 0;
        this.position[moveSquares.to.y][moveSquares.to.x] = movingPiece;
    }

    checkCastlingRule(moveSquares) {
        let movingPiece = this.position[moveSquares.from.y][moveSquares.from.x];
        if (movingPiece !== Wking && movingPiece !== Bking)
            return;

        let delta = moveSquares.to.x - moveSquares.from.x;
        if (Math.abs(delta) > 1) {
            let rookFromX = delta < 0 ? 0 : 7;
            let rookToX = delta < 0 ? 3 : 5;

            let rookMove = {
                from: { x: rookFromX, y: moveSquares.from.y },
                to: { x: rookToX, y: moveSquares.from.y }
            };
            this.moveOnePiece(rookMove);
        }
    }

    checkEnPassantRule(moveSquares) {
        let movingPiece = this.position[moveSquares.from.y][moveSquares.from.x];
        if (movingPiece !== Wpawn && movingPiece !== Bpawn)
            return;

        let delta = moveSquares.to.x - moveSquares.from.x;
        if (delta === 0) // i.e. move is not capture
            return;

        // find target piece; if it's empty, then apply en passant rule
        let targetPiece = this.position[moveSquares.to.y][moveSquares.to.x];
        if (targetPiece === 0) {
            let capturedY = moveSquares.from.y;
            let capturedX = moveSquares.to.x;
            this.position[capturedY][capturedX] = 0;
        }
    }

    nextMove() {
        let nextTurn = this.getNextTurn();
        let nextMoveNumber = nextTurn === TurnWhite ? this.currentMoveNumber + 1 : this.currentMoveNumber;
        this.currentTurn = nextTurn;
        this.currentMoveNumber = nextMoveNumber;
    }

    isValidCurrentMoveNumber() {
        return this.isValidMoveNumber(this.currentMoveNumber);
    }

    isValidMoveNumber(moveNumber) {
        return moveNumber > 0 && moveNumber <= this.moves.length;
    }

    getNextTurn() {
        return (TurnWhite + TurnBlack) - this.currentTurn;
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
        return '<img align="center" width="100%" height="100%" src="/Content/Images/' + this.getImageName(piece) + '">';
    }

    renderPosition() {
        for (let y = 1; y <= 8; y++) {
            for (let x = 1; x <= 8; x++) {
                let cellId = this.getCellId(x, y);
                let cell = $("#" + cellId);
                let innerHtml = this.getInnerHtml(this.game.position[y - 1][x - 1]);
                if (innerHtml !== cell.html()) {
                    cell.html(innerHtml);
                }
            }
        }

        var notationRoot = $("#game-notation");
        notationRoot.find("td").css("font-weight", "normal");
        let moveNumberCell = $("#notation-number-" + this.game.currentMoveNumber);
        let turnNumberCell = $("#notation-" + (this.game.currentTurn === TurnWhite ? "white" : "black") + "-" + + this.game.currentMoveNumber);
        moveNumberCell.css("font-weight", " bold");
        turnNumberCell.css("font-weight", " bold");
        if (moveNumberCell.length > 0) {
            let rectContainer = notationRoot[0].getBoundingClientRect();
            let rectElem = moveNumberCell[0].getBoundingClientRect();
            if (rectElem.bottom > rectContainer.bottom) {
                moveNumberCell[0].scrollIntoView(false);
            }
            else if (rectElem.top < rectContainer.top) {
                moveNumberCell[0].scrollIntoView();
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

        $("#game-notation").find("td").click(event => {
            let id = event.target.id.replace("notation-number", "notation-white");
            let moveNumber = parseInt(id.match(/\d+/));
            let turn = id.startsWith("notation-white") ? TurnWhite : TurnBlack;
            this.renderer.game.setMove(moveNumber, turn);
            this.renderer.renderPosition();
            event.stopPropagation();
        });
    }

    play() {
        this.stepForward();
        this.playTimer = setInterval(() => this.stepForward(), 1000);
    }

    stop() {
        if (this.playTimer) {
            clearInterval(this.playTimer);
            delete this.playTimer;
        }
    }

    stepForward() {
        this.renderer.game.makeMove();
        this.renderer.renderPosition();
        if (this.playTimer && !this.renderer.game.isValidCurrentMoveNumber()) {
            this.stop()
        }
    }

    stepBackward() {
        this.renderer.game.backMove();
        this.renderer.renderPosition();
    }
}

function doShowGame(moves) {
    let game = new ChessGame(moves);
    let renderer = new GameRenderer(game);
    renderer.renderPosition();
    let events = new PlayGameEvents(renderer);
    events.bindEvents();
}
