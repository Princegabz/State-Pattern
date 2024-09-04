using System;

namespace MusicPlayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new music player instance.
            MusicPlayers player = new MusicPlayers();

            // Transition through different states by invoking player actions.
            player.Play();  // Should transition from StoppingState to PlayingState
            player.Stop();  // Should transition from PlayingState to StoppingState
            player.Pause(); // Should not transition because the player is already stopped

            Console.ReadLine(); // Wait for user input before closing the console
        }

        // Define an interface that all state classes must implement. 
        // This interface declares the actions that can be performed on the music player.
        interface IMusicPLayerState
        {
            void Play(MusicPlayers player);  // Triggered when the play action is performed
            void Pause(MusicPlayers player); // Triggered when the pause action is performed
            void Stop(MusicPlayers player);  // Triggered when the stop action is performed
        }

        // The 'MusicPlayers' class is the Context class. It maintains a reference to a state object
        // that defines the current state of the music player.
        class MusicPlayers
        {
            // This property holds the current state of the music player.
            public IMusicPLayerState CurrentState { get; set; }

            // Constructor that initializes the music player in the 'StoppingState'.
            public MusicPlayers()
            {
                CurrentState = new StoppingState();
            }

            // Method to change the current state of the music player.
            public void ChangeState(IMusicPLayerState state)
            {
                CurrentState = state;
            }

            // Method to trigger the 'Play' action in the current state.
            public void Play()
            {
                CurrentState.Play(this);
            }

            // Method to trigger the 'Pause' action in the current state.
            public void Pause()
            {
                CurrentState.Pause(this);
            }

            // Method to trigger the 'Stop' action in the current state.
            public void Stop()
            {
                CurrentState.Stop(this);
            }
        }

        // The 'PlayingState' class represents the state of the music player when it is playing a song.
        // It implements the IMusicPLayerState interface and defines the behavior for play, pause, and stop actions.
        class PlayingState : IMusicPLayerState
        {
            // When the 'Play' action is invoked while in the playing state,
            // it simply informs that the song is already playing.
            public void Play(MusicPlayers player)
            {
                Console.WriteLine("Song is already playing");
            }

            // When the 'Pause' action is invoked while in the playing state,
            // it transitions the player to the 'PausedState'.
            public void Pause(MusicPlayers player)
            {
                Console.WriteLine("Song is paused");
                player.ChangeState(new PausedState()); // Transition to PausedState
            }

            // When the 'Stop' action is invoked while in the playing state,
            // it transitions the player to the 'StoppingState'.
            public void Stop(MusicPlayers player)
            {
                Console.WriteLine("Song has stopped playing");
                player.ChangeState(new StoppingState()); // Transition to StoppingState
            }
        }

        // The 'PausedState' class represents the state of the music player when it is paused.
        class PausedState : IMusicPLayerState
        {
            // When the 'Play' action is invoked while in the paused state,
            // it resumes playing and transitions the player to the 'PlayingState'.
            public void Play(MusicPlayers player)
            {
                Console.WriteLine("Resuming song playback");
                player.ChangeState(new PlayingState()); // Transition to PlayingState
            }

            // When the 'Pause' action is invoked while in the paused state,
            // it informs that the song is already paused and does not change the state.
            public void Pause(MusicPlayers player)
            {
                Console.WriteLine("Song is already paused");
            }

            // When the 'Stop' action is invoked while in the paused state,
            // it stops the song and transitions the player to the 'StoppingState'.
            public void Stop(MusicPlayers player)
            {
                Console.WriteLine("Song has stopped playing");
                player.ChangeState(new StoppingState()); // Transition to StoppingState
            }
        }

        // The 'StoppingState' class represents the state of the music player when it is stopped.
        class StoppingState : IMusicPLayerState
        {
            // When the 'Play' action is invoked while in the stopped state,
            // it starts playing the song and transitions the player to the 'PlayingState'.
            public void Play(MusicPlayers player)
            {
                Console.WriteLine("Song is now playing");
                player.ChangeState(new PlayingState()); // Transition to PlayingState
            }

            // When the 'Pause' action is invoked while in the stopped state,
            // it informs that the song is already paused and does not change the state.
            public void Pause(MusicPlayers player)
            {
                Console.WriteLine("Cannot pause. The song is not playing");
            }

            // When the 'Stop' action is invoked while in the stopped state,
            // it informs that the song is already stopped and does not change the state.
            public void Stop(MusicPlayers player)
            {
                Console.WriteLine("Song is already stopped");
            }
        }
    }
}
