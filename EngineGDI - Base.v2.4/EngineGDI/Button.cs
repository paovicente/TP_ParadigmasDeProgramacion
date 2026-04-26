namespace EngineGDI
{
    public class Button
    {
        public string Label { get; private set; }
        public MenuAction Action { get; private set; }
        public bool IsSelected { get; private set; }
        public string TexturePath { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Scale { get; private set; }

        public Button(string label, MenuAction action, string texturePath, Vector2 position, Vector2 scale)
        {
            Label = label;
            Action = action;
            IsSelected = false;
            TexturePath = texturePath;
            Position = position;
            Scale = scale;
        }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
        }
    }
}
