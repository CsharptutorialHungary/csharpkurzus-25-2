namespace MovieBox.Core.Records
{
    /// <summary>
    /// Represents the status of a movie in the collection.
  /// </summary>
    public enum MovieStatus
    {
        /// <summary>
        /// The movie has been watched/seen.
        /// </summary>
   Seen = 1,

    /// <summary>
        /// The movie is marked as a favorite.
    /// </summary>
        Favorite = 2,

   /// <summary>
  /// The movie is in the watchlist.
   /// </summary>
    Watchlist = 3
    }
}
