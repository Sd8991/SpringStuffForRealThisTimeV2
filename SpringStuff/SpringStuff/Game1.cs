﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpringOperations sO;
    EndPointTracker t1,t2;
    Spring s1, s2, s3;
    Rectangle r;
    Texture2D SpringPic, DotPic;
    Spring[] allSpring;
    Rectangle[] allRectangle;
    EndPointTracker[] allTracker;
    double thyme;


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
        s1 = new Spring(new Vector2(100, 0), new Vector2(100, 150), 100, 20, 0.0f, 1f, 5, 0f);
        allSpring[0] = s1;
        s2 = new Spring(new Vector2(100, 150), new Vector2(100, 300), 100, 20, 0.0f, 0.5f, 5, 0f);
        allSpring[1] = s2;
        s3 = new Spring(new Vector2(200, 0), new Vector2(200, 300), 200, 20, 0.0f, 1f, 5, 0f);
        allSpring[2] = s3;
        sO = new SpringOperations();
        t1 = new EndPointTracker(s3, new Dictionary<Vector3, int>());
        allTracker[0] = t1;
        t2 = new EndPointTracker(s2, new Dictionary<Vector3, int>());
        allTracker[1] = t2;
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

        foreach (Spring s in allSpring)
        {
            //s.SLS(gameTime);
            //s.SLSMkII(gameTime);
        }
        s3.SLSMkII(thyme);
        sO.MultiSpring(thyme, s1, s2);
        foreach(EndPointTracker e in allTracker) e.Track();


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
            r = new Rectangle((int)allSpring[2].beginPointX, (int)allSpring[2].beginPointY, (int)allSpring[2].radius, (int)(allSpring[2].stretch + allSpring[2].restLength));
            allRectangle[2] = r;
        }
        if(gameTime.TotalGameTime.Milliseconds%500 == 0)
        {
            for (int i = 1; i <= allSpring.Length; i++)
            {
                Console.Write("s{0} bPoint ({1}, {2}); ePoint ({3}, {4}) |", i, (int)allSpring[i - 1].beginPointX, (int)allSpring[i - 1].beginPointY, (int)allSpring[i - 1].endPointX, (int)allSpring[i - 1].endPointY);
            }
            Console.WriteLine();
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
        foreach (EndPointTracker e in allTracker)
            foreach (Vector3 v in e.points.Keys)
                spriteBatch.Draw(DotPic, new Vector2((float)(v.X + 0.5*e.s.radius), v.Y), new Color(200, Math.Min(e.points[v], 200), Math.Min(e.points[v], 200)));
        spriteBatch.End();
        base.Draw(gameTime);
    }
}