using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpringOperations sO;
    SimulationTest sT;
    EndPointTracker t1,t2;
    Spring s1, s2, s3;
    Rectangle r;
    Texture2D SpringPic, DotPic;
    Spring[] allSpring;
    Rectangle[] allRectangle;
    EndPointTracker[] allTracker;
    double thyme, totalThyme;
    int testCases = 30;
    int currentCase = 0;
    bool isInit = false;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        allSpring = new Spring[3];
        allRectangle = new Rectangle[3];
        allTracker = new EndPointTracker[2];
        s1 = new Spring(new Vector2(100, 0), new Vector2(100, 50), 50, 50, 25f, 1f, 5f, 0.5f);
        allSpring[0] = s1;
        s2 = new Spring(new Vector2(100, 50), new Vector2(100, 200), 150, 50, 25f, 1f, 5f, 0.5f);
        allSpring[1] = s2;
        s3 = new Spring(new Vector2(250, 0), new Vector2(250, 200), 200, 50, 50f, 1f, 5, 0.5f);
        allSpring[2] = s3;
        sO = new SpringOperations();
        sT = new SimulationTest();
        //t1 = new EndPointTracker(s3, new Dictionary<Vector3, int>());
        //allTracker[0] = t1;
        //t2 = new EndPointTracker(s2, new Dictionary<Vector3, int>());
       // allTracker[1] = t2;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);
        SpringPic = Content.Load<Texture2D>("Spring_Mk_II");
        DotPic = Content.Load<Texture2D>("DotBoi");
    }

    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }

    protected override void Update(GameTime gameTime)
    {
        thyme = (double)gameTime.ElapsedGameTime.Milliseconds / 1000;

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        allSpring[2].SLSMkII(thyme);
        sO.MultiSpring(thyme, allSpring[0], allSpring[1]);
        //foreach(EndPointTracker e in allTracker) e.Track();


        for (int i = 0; i < allSpring.Length - 1; i++)
        {
            try
            {
                r = new Rectangle((int)allSpring[i].beginPointX, (int)allSpring[i - 1].endPointY + (int)allSpring[i - 1].beginPointY, (int)allSpring[i].radius, (int)(allSpring[i].stretch + allSpring[i].restLength));
                allRectangle[i] = r;
            }
            catch
            {
                r = new Rectangle((int)allSpring[i].beginPointX, (int)allSpring[i].beginPointY, (int)allSpring[i].radius, (int)(allSpring[i].stretch + allSpring[i].restLength));
                allRectangle[i] = r;
            }
        }
        r = new Rectangle((int)allSpring[2].beginPointX, (int)allSpring[2].beginPointY, (int)allSpring[2].radius, (int)(allSpring[2].stretch + allSpring[2].restLength));
        allRectangle[2] = r;

        if (currentCase <= testCases)
        {
            sT.theTest(allSpring, thyme, currentCase, isInit);
        }

        if (sT.hasFinished)
        {
            s1 = new Spring(new Vector2(100, 0), new Vector2(100, 100), 100, 50, 0.0f, 1f, 5f, 0.5f);
            allSpring[0] = s1;
            s2 = new Spring(new Vector2(100, 100), new Vector2(100, 200), 100, 50, 0.0f, 1f, 2.5f, 1f);
            allSpring[1] = s2;
            s3 = new Spring(new Vector2(200, 0), new Vector2(250, 200), 200, 50, 0.0f, 1f, 5, 0.5f);
            allSpring[2] = s3;
            sT.totalThyme = 0;
            sT.hasFinished = true;
        }
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // TODO: Add your drawing code here
        spriteBatch.Begin();
        foreach (Rectangle r in allRectangle)
            spriteBatch.Draw(SpringPic, r, Color.AntiqueWhite);
        //foreach (EndPointTracker e in allTracker)
            //foreach (Vector3 v in e.points.Keys)
                //spriteBatch.Draw(DotPic, new Vector2((float)(v.X + 0.5*e.s.radius), v.Y), new Color(200, Math.Min(e.points[v], 200), Math.Min(e.points[v], 200)));
        spriteBatch.End();
        base.Draw(gameTime);
    }
}