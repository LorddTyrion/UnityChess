using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RootScript : MonoBehaviour
{
    public Board b;
    private bool first = true;
    private bool firstEsc = true;
    private bool endgame = false;
    private int prevx, prevy;
    private GameObject[,] buttons=new GameObject[8,8];
    public GameObject WPawn;
    public GameObject BPawn;
    public GameObject WBishop;
    public GameObject BBishop;
    public GameObject WKnight;
    public GameObject BKnight;
    public GameObject WRook;
    public GameObject BRook;
    public GameObject WQueen;
    public GameObject BQueen;
    public GameObject WKing;
    public GameObject BKing;
    private List<GameObject> list = new List<GameObject>();
    const float baseX = -5.6f, baseY=0.55f, baseZ = 3.9f;

    public GameObject newGameButton;
    public GameObject exitButton;
    public GameObject textBox;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        textBox.GetComponent<TextMeshProUGUI>().text = "Welcome to chess! \nPress esc to bring up menu anytime!";
        newGameButton.GetComponent<Button>().onClick.AddListener(() => StartGame());
        exitButton.GetComponent<Button>().onClick.AddListener(() => Application.Quit());


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (firstEsc) {
                canvas.SetActive(true);
                firstEsc = false;
            }
            else if(!endgame)
            {
                canvas.SetActive(false);
                firstEsc = true;
            }
            
        }
    }
    void StartGame()
    {

        canvas.SetActive(false);
        endgame = false;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                buttons[7 - i, j] = transform.GetChild(1).GetChild(i * 8 + j).gameObject;
                Debug.Log(((7 - i) * 8 + j) + " i:" + i + " j:" + j);
                int a = 7 - i, b = j;
                buttons[7 - i, j].GetComponent<Button>().onClick.AddListener(() => Click(a, b));

            }
        }
        b = new Board();
        GenerateBoard();
        resetColors();



    }
    void Click(int x, int y)
    {
        Debug.Log(x + " " + y);
        if (first)
        {            
            List<Move> possibleMoves = b.getPossibleMoves(x, y);
            if (possibleMoves.Count > 0)
            {
                prevx = x;
                prevy = y;
                first = false;
            }
            for (int i = 0; i < possibleMoves.Count; i++)
            {
                int possibleX = possibleMoves[i].TargetX;
                int possibleY = possibleMoves[i].TargetY;
                buttons[possibleX, possibleY].GetComponent<Image>().color = new UnityEngine.Color(0, 0, 255, 255);
            }
        }
        else
        {
            bool result=b.Move(prevx, prevy, x, y);
            if (result)
            {
                GenerateBoard();
                if (b.CheckEndGame() == Color.WHITE)
                {
                    textBox.GetComponent<TextMeshProUGUI>().text = "White wins!";
                    canvas.SetActive(true);
                    endgame = true;
                }
                else if(b.CheckEndGame() == Color.BLACK)
                {
                    textBox.GetComponent<TextMeshProUGUI>().text = "Black wins!";
                    canvas.SetActive(true);
                    endgame = true;
                }
                else if (b.CheckEndGame() == Color.DRAW)
                {
                    textBox.GetComponent<TextMeshProUGUI>().text = "Draw!";
                    canvas.SetActive(true);
                    endgame = true;
                }
            }
            first = true;
            resetColors();
        }
    }
    void resetColors()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                buttons[i, j].GetComponent<Image>().color = new UnityEngine.Color(0, 0, 255, 0);
            }
            
        }
    }
    void GenerateBoard()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
        }
        list.Clear();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece p = b.boardState.squares[i, j].Piece;
                if (p == null) continue;
                switch (p.PieceName)
                {
                    case PieceName.PAWN:
                        if (p.IsWhite)
                        {
                            list.Add(Instantiate(WPawn, new Vector3(baseX+j, baseY, baseZ+i), Quaternion.identity));
                        }
                        else
                        {
                            list.Add(Instantiate(BPawn, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        break;
                    case PieceName.KING:
                        if (p.IsWhite)
                        {
                            list.Add(Instantiate(WKing, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        else
                        {
                            list.Add(Instantiate(BKing, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        break;
                    case PieceName.QUEEN:
                        if (p.IsWhite)
                        {
                            list.Add(Instantiate(WQueen, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        else
                        {
                            list.Add(Instantiate(BQueen, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        break;
                    case PieceName.ROOK:
                        if (p.IsWhite)
                        {
                            list.Add(Instantiate(WRook, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        else
                        {
                            list.Add(Instantiate(BRook, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        break;
                    case PieceName.BISHOP:
                        if (p.IsWhite)
                        {
                            list.Add(Instantiate(WBishop, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        else
                        {
                            list.Add(Instantiate(BBishop, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        break;
                    case PieceName.KNIGHT:
                        if (p.IsWhite)
                        {
                            list.Add(Instantiate(WKnight, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.identity));
                        }
                        else
                        {
                            list.Add(Instantiate(BKnight, new Vector3(baseX + j, baseY, baseZ + i), Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f))));
                        }
                        break;
                    default:
                        break;

                }
            }
        }
    }

}
