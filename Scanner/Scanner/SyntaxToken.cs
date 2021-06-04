namespace Scanner
{
    class SyntaxToken
    {
        public SyntaxToken(SyntaxKind kind, int position, string text)
        {
            Kind = kind;
            Position = position;
            Text = text;
        }
        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
    }
}
