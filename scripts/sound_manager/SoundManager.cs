using Godot;

public partial class SoundManager : Node2D
{
	private AudioStreamPlayer coin_sound;
	private AudioStreamPlayer thunkSound;
	private AudioStreamPlayer choppedSound;
	public override void _Ready()
	{
		coin_sound = GetNode<AudioStreamPlayer>("CoinSound");
		thunkSound = GetNode<AudioStreamPlayer>("ThunkSound");
		choppedSound = GetNode<AudioStreamPlayer>("ChoppedSound");
	}

	// PLAY DIFFERENT SOUNDS
	public void PlayCoinSound()
	{
		coin_sound.Play();
	}
	public void PlayThunkSound()
	{
		thunkSound.Play();
	}
	public void PlayChoppedSound()
	{
		choppedSound.Play();
	}
}
