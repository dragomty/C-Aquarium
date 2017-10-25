using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;              // Required to use XNA features.
using XNAMachinationisRatio;                // Required to use the XNA Machinationis Ratio Engine general features.
using XNAMachinationisRatio.AI;             // Required to use the XNA Machinationis Ratio general AI features.
using System.Timers;




/* LERNING PILL: XNAMachinationisRatio Engine
 * XNAMachinationisRatio is an engine that allows implementing
 * simulations and games based on XNA, simplifying the use of XNA
 * and adding features not directly available in XNA.
 * XNAMachinationisRatio is a work in progress.
 * The engine works "under the hood", taking care of many features
 * of an interactive simulation automatically, thus minimizing
 * the amount of code that developers have to write.
 * 
 * In order to use the engine, the application main class (Kernel, in the
 * case of FishO'Rama) creates, initializes and stores
 * an instance of class Engine in one of its data members.
 * 
 * The classes comprised in the  XNA Machinationis Ratio engine and the
 * related functionalities can be accessed from any of your XNA project
 * source code files by adding appropriate 'using' statements at the beginning of
 * the file. 
 * 
 */

namespace FishORama
{
    /* LEARNING PILL: Token behaviors in the XNA Machinationis Ratio engine
     * Some simulation tokens may need to enact specific behaviors in order to
     * participate in the simulation. The XNA Machinationis Ratio engine
     * allows a token to enact a behavior by associating an artificial intelligence
     * mind to it. Mind objects are created from subclasses of the class AIPlayer
     * included in the engine. In order to associate a mind to a token, a new
     * mind object must be created, passing to the constructor of the mind a reference
     * of the object that must be associated with the mind. This must be done in
     * the DefaultProperties method of the token.
     * 
     * Hence, every time a new tipe of AI mind is required, a new class derived from
     * AIPlayer must be created, and an instance of it must be associated to the
     * token classes that need it.
     * 
     * Mind objects enact behaviors through the method Update (see below for further details). 
     */
    class SeaHorseMind : AIPlayer
    {
        #region Data Members

        // This mind needs to interact with the token which it possesses, 
        // since it needs to know where are the aquarium's boundaries.
        // Hence, the mind needs a "link" to the aquarium, which is why it stores in
        // an instance variable a reference to its aquarium.
        private AquariumToken mAquarium;        // Reference to the aquarium in which the creature lives.

        private float mFacingDirection;         // Direction the fish is facing (1: right; -1: left).\
        private float YDirection;
        private float XDirection;
        private int mSpeed;
        private int rnd;
        private int rspeed;
        private bool randomevent = true;//////random bool which can valid or not
        private int SwimPositionMinus;
        private int SwimPositionPlus;
        private int ScatterPositionY;
        private int ScatterPositionX;
        private bool Position = true;
        private int maxspeed = 15;
        private bool Scatter = false;

        private float rndDirx, rndDiry;
                
    
        #endregion

        #region Properties

        /// <summary>
        /// Set Aquarium in which the mind's behavior should be enacted.
        /// </summary>
        public AquariumToken Aquarium
        {
            set { mAquarium = value; }

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="pToken">Token to be associated with the mind.</param>
        public SeaHorseMind(X2DToken pToken)
        {
            /* LEARNING PILL: associating a mind with a token
             * In order for a mind to control a token, it must be associated with the token.
             * This is done when the mind is constructed, using the method Possess inherited
             * from class AIPlayer.
             */
            this.Possess(pToken);       // Possess token.
            mFacingDirection = 1;       // Current direction the fish is facing.
            //int turns = 1;///turns variable?
            Random rnd1 = new Random();
            int LeftRight = rnd1.Next(1, 10);

            if (LeftRight > 9)///generating directions
            {
                YDirection = 1;
            }

            else
            {
                YDirection = -1;

            }
            Random rndDir = new Random();
            rndDirx = (float)rndDir.NextDouble();////transform double to float//////////
            Random rndMinDirection = new Random();



            Random rnd2 = new Random();////////////////////////////2 directions
            int rndMinusDirection = rnd2.Next(1, 10);

            if (rndMinusDirection < 5)////
            {
                rndDirx = -1;////generate  x 
            }


            rndDiry = (float)rndDir.NextDouble();////pakeis
            rndMinusDirection = rnd2.Next(1, 10);

            if (rndMinusDirection < 5)
            {
                rndDiry = -1;////generate y
            }

     
        }


        #endregion
        #region Methods

        /// <summary>
        /// AI Update method.
        /// </summary>
        /// <param name="pGameTime">Game time</param>
        public override void Update(ref GameTime pGameTime)
        {

            Vector3 tokenPosition = this.PossessedToken.Position;


            if (Scatter == true)
            {
                ScatterPositionY = (int)this.PossessedToken.Position.Y;////////
                ScatterPositionX = (int)this.PossessedToken.Position.X;////////
                tokenPosition = ScatterBehaviour(tokenPosition);/////////////kviecia scatter
                //Scatter = false;////////////////neali buti false kol nedingsta
            }
          
            

            if (Position == true)
            {
                SwimPositionMinus = (int) this.PossessedToken.Position.Y - 100; //minus
                SwimPositionPlus = (int)this.PossessedToken.Position.Y + 100; //plus int
                Position = false;
            }
            

            if (randomevent == true)
            {
                rspeed = gRandom(4, 10);
                mSpeed = rspeed;
            }

            if (mAquarium.ChickenLeg != null)/////////////////////jei koja yra 
            {

                Scatter = true;
                tokenPosition = ScatterBehaviour(tokenPosition);/////////////kviecia scatter
            }

            else if (mAquarium.ChickenLeg == null)
            {
                Random rndDir = new Random();
                rndDirx = (float)rndDir.NextDouble();////pakeisti double i float//////////
                Random rndMinDirection = new Random();



                Random rnd2 = new Random();////////////////////////////2 directions
                int rndMinusDirection = rnd2.Next(1, 10);

                if (rndMinusDirection < 5)////??????????????
                {
                    rndDirx = -1;////generate  x 
                }


                rndDiry = (float)rndDir.NextDouble();////pakeisti double i float//////////
                rndMinusDirection = rnd2.Next(1, 10);

                if (rndMinusDirection < 5)
                {
                    rndDiry = -1;////generate y
                }
                
                Scatter = false;
                tokenPosition = HorizontalSwimBehaviour(tokenPosition);

            }
            //tokenPosition = HorizontalSwimBehaviour(tokenPosition);
           // tokenPosition = ScatterBehaviour(tokenPosition);
            
          
            
            /* if (rspeed == 1)
            {
                mSpeed = 5;
                randomevent = false;
                tokenPosition = VerticalSwimBehaviour(tokenPosition);
            }

            else if (rspeed == 2)
            {
                mSpeed = 10;
                randomevent = false;
                tokenPosition = HorizontalSwimBehaviour(tokenPosition);
            }*/


        }

        public Vector3 HorizontalSwimBehaviour(Vector3 tokenPosition)//this is fine
        {
            //this.PossessedToken.Orientation = new Vector3(mFacingDirection, this.PossessedToken.Orientation.Y, this.PossessedToken.Orientation.Z);
            tokenPosition.X = tokenPosition.X + mSpeed * mFacingDirection;///do it so it wont go -4 speed allows to increase speed
            tokenPosition.Y = tokenPosition.Y + mSpeed * YDirection;


            if (tokenPosition.Y >= SwimPositionPlus)///
            {
               YDirection *= -1;
               tokenPosition.Y = tokenPosition.Y + mSpeed * YDirection;
               this.PossessedToken.Orientation = new Vector3(mFacingDirection, YDirection, this.PossessedToken.Orientation.Z);
            }

            if (tokenPosition.Y <= SwimPositionMinus)
            {
                YDirection *= -1;
                tokenPosition.Y = tokenPosition.Y + mSpeed * YDirection;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, YDirection, this.PossessedToken.Orientation.Z);
            }
            
            if (tokenPosition.X >= 400 || tokenPosition.X <= -400)
            {
                mFacingDirection *= -1;
                this.PossessedToken.Orientation = new Vector3(mFacingDirection, YDirection, this.PossessedToken.Orientation.Z);
            }
            this.PossessedToken.Position = tokenPosition;////new
            return tokenPosition;// returns tokenposition
        }

        private int gRandom(int min, int max)//////////////////generating number///////////
        {
            Random rnd = new Random();
            int rspeed = rnd.Next(5, 11);///generates 1 or 2 for generating seahorse speed
            return rspeed;
  
        }

        public Vector3 ScatterBehaviour(Vector3 tokenPosition)///////issisladimo budas
        {
           
           // if (tokenPosition.Y < ScatterPositionY + l && tokenPosition.Y > ScatterPositionY -  && tokenPosition.X < ScatterPositionX + 100 && tokenPosition.X > ScatterPositionX - 100)///kad neislistu uz ribu
         //   {
              
              // rndDirx = 1;
              // rndDiry = 1;
                tokenPosition.X = tokenPosition.X + maxspeed * rndDirx;//
                tokenPosition.Y = tokenPosition.Y + maxspeed * rndDiry;
               // this.PossessedToken.Orientation = new Vector3(rndDirx, rndDiry, this.PossessedToken.Orientation.Z);


                if (tokenPosition.Y >= 230 || tokenPosition.Y <= -230)///
               {
                   rndDiry *= 0;
                   rndDirx = 0;
                   Console.WriteLine("cccc");
                  // tokenPosition.Y = tokenPosition.Y + maxspeed * YDirection;
                   this.PossessedToken.Orientation = new Vector3(rndDirx, rndDiry, this.PossessedToken.Orientation.Z);
               }


             /*  if (tokenPosition.Y <= -250)
               {
                  // maxspeed = 0;
                   rndDiry *= 0;
                   rndDirx = 0;
                 //  Console.WriteLine("bbbbb");
                  // tokenPosition.Y = tokenPosition.Y + maxspeed * YDirection;
                   this.PossessedToken.Orientation = new Vector3(rndDirx, rndDiry, this.PossessedToken.Orientation.Z);
               }*/


                if (tokenPosition.X >= 280 || tokenPosition.X <= -280)
                {
                   // maxspeed = 0;
                    rndDirx *= 0;
                    rndDiry = 0;
                   // Console.WriteLine("vvvvv");
                    this.PossessedToken.Orientation = new Vector3(rndDirx, rndDiry, this.PossessedToken.Orientation.Z);
                }
                this.PossessedToken.Position = tokenPosition;
                return tokenPosition;// returns tokenposition


        }
    }
}


        #endregion









