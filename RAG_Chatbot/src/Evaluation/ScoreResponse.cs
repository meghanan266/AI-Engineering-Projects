namespace Evaluation;

class ScoreResponse
{
    public string? Justification { get; set; }
    public ScoreLabel ScoreLabel { get; set; }

    public double ScoreNumber => ScoreLabel switch
    {
        ScoreLabel.Awful => 0,
        ScoreLabel.Poor => 0.3,
        ScoreLabel.Good => 0.7,
        ScoreLabel.Perfect => 1,
        _ => throw new InvalidOperationException("Invalid score label")
    };
}

enum ScoreLabel { Awful, Poor, Good, Perfect }
