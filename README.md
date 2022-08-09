# Trivia-Game
 
GameManager
-Oyunun genel işlemleri burada gerçekleştirilir (başlama, bitiş)
-Menu ekranından kategori ekranına geçiş
-Kategoriler ekranında arama yapma (yapılacak)
-Daha önce seçimi yapılmış kategoriden(persist) direk oyunu başlatma 

CategoryScreen:
-Kategorileri "Apihelper static" classının yardımıyla çeker. Veri tipleri "Categories" classında belirtilmiştir
-Seçilen kategori için seçimi hatırlama(persist)

QuestionManager:
-Seçilen kategoriye göre soruları "ApiHelper static" classının yardımıyla çeker (fetching). Veri tipleri "Question" classında belirtilmiştir
-Soruları Uida yazdırır
-Herhangi bir cevaba tıklanması durumda (doğru, yanlış, boş bırakma) işlem gerçekleştirir
-Sonraki soru ekranına geçişi sağlar
-Görsellik (yazı renkleri animasyon)
