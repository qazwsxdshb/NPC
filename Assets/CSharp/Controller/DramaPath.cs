namespace CSharp.Controller {
    public struct DramaPath {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public static class DramaPaths {
        // For Resources.Load<TextAsset>(path)
        public static readonly DramaPath Hard = new DramaPath
            { Name = "Hard", Path = "Drama/modified/modified-hard-2" };

        public static readonly DramaPath Normal = new DramaPath
            { Name = "Normal", Path = "Drama/modified/modified-normal-2" };
    }
}
