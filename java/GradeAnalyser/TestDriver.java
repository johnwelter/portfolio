package GradeAnalyser;


public class TestDriver {
	
	
	public static void main(String args[])
	{
		
		TestBatch serifA = new TestBatch("serifA");
		TestBatch serifB = new TestBatch("serifB");
		TestBatch sansA = new TestBatch("sansA");
		TestBatch sansB = new TestBatch("sansB");
		
		serifA.add(new Test(4, 4.0, "A01"));
		serifA.add(new Test(4, 4.0, "A02"));
		serifA.add(new Test(4, 4.0, "A03"));
		serifA.add(new Test(4, 4.0, "A04"));
		serifA.add(new Test(4, 4.0, "A05"));
		serifA.add(new Test(4, 4.0, "A06"));
		serifA.add(new Test(4, 4.0, "A07"));
		serifA.add(new Test(4, 4.0, "A08"));
		serifA.add(new Test(4, 4.0, "A09"));
		serifA.add(new Test(4, 4.0, "A10"));
		
		sansA.add(new Test(4, 4.0, "A01"));
		sansA.add(new Test(4, 4.0, "A02"));
		sansA.add(new Test(4, 4.0, "A03"));
		sansA.add(new Test(4, 4.0, "A04"));
		sansA.add(new Test(4, 4.0, "A05"));
		sansA.add(new Test(4, 4.0, "A06"));
		sansA.add(new Test(4, 4.0, "A07"));
		sansA.add(new Test(4, 4.0, "A08"));
		sansA.add(new Test(4, 4.0, "A09"));
		sansA.add(new Test(4, 4.0, "A10"));
		
		serifB.add(new Test(4, 4.0, "B01"));
		serifB.add(new Test(4, 4.0, "B02"));
		serifB.add(new Test(4, 4.0, "B03"));
		serifB.add(new Test(4, 4.0, "B04"));
		serifB.add(new Test(4, 4.0, "B05"));
		serifB.add(new Test(4, 4.0, "B06"));
		serifB.add(new Test(4, 4.0, "B07"));
		serifB.add(new Test(4, 4.0, "B08"));
		serifB.add(new Test(4, 4.0, "B09"));
		serifB.add(new Test(4, 4.0, "B10"));
		
		sansB.add(new Test(4, 4.0, "B01"));
		sansB.add(new Test(4, 4.0, "B02"));
		sansB.add(new Test(4, 4.0, "B03"));
		sansB.add(new Test(4, 4.0, "B04"));
		sansB.add(new Test(4, 4.0, "B05"));
		sansB.add(new Test(4, 4.0, "B06"));
		sansB.add(new Test(4, 4.0, "B07"));
		sansB.add(new Test(4, 4.0, "B08"));
		sansB.add(new Test(4, 4.0, "B09"));
		sansB.add(new Test(4, 4.0, "B10"));
		
		//fill serifA tests
		serifA.getTestAt(0).setAnswerAt(0, 1.0);
		serifA.getTestAt(0).setAnswerAt(1, 0.0);
		serifA.getTestAt(0).setAnswerAt(2, 0.0);
		serifA.getTestAt(0).setAnswerAt(3, 0.0);
		
		serifA.getTestAt(1).setAnswerAt(0, 1.0);
		serifA.getTestAt(1).setAnswerAt(1, 1.0);
		serifA.getTestAt(1).setAnswerAt(2, 1.0);
		serifA.getTestAt(1).setAnswerAt(3, 0.0);
		
		serifA.getTestAt(2).setAnswerAt(0, 1.0);
		serifA.getTestAt(2).setAnswerAt(1, 0.0);
		serifA.getTestAt(2).setAnswerAt(2, 0.0);
		serifA.getTestAt(2).setAnswerAt(3, 0.0);
		
		serifA.getTestAt(3).setAnswerAt(0, 1.0);
		serifA.getTestAt(3).setAnswerAt(1, 1.0);
		serifA.getTestAt(3).setAnswerAt(2, 1.0);
		serifA.getTestAt(3).setAnswerAt(3, 1.0);
		
		serifA.getTestAt(4).setAnswerAt(0, 1.0);
		serifA.getTestAt(4).setAnswerAt(1, 0.0);
		serifA.getTestAt(4).setAnswerAt(2, 0.0);
		serifA.getTestAt(4).setAnswerAt(3, 1.0);
		
		serifA.getTestAt(5).setAnswerAt(0, 1.0);
		serifA.getTestAt(5).setAnswerAt(1, 1.0);
		serifA.getTestAt(5).setAnswerAt(2, 0.0);
		serifA.getTestAt(5).setAnswerAt(3, 1.0);
		
		serifA.getTestAt(6).setAnswerAt(0, 1.0);
		serifA.getTestAt(6).setAnswerAt(1, 0.0);
		serifA.getTestAt(6).setAnswerAt(2, 0.0);
		serifA.getTestAt(6).setAnswerAt(3, 1.0);
		
		serifA.getTestAt(7).setAnswerAt(0, 1.0);
		serifA.getTestAt(7).setAnswerAt(1, 1.0);
		serifA.getTestAt(7).setAnswerAt(2, 1.0);
		serifA.getTestAt(7).setAnswerAt(3, 0.0);
		
		serifA.getTestAt(8).setAnswerAt(0, 1.0);
		serifA.getTestAt(8).setAnswerAt(1, 1.0);
		serifA.getTestAt(8).setAnswerAt(2, 1.0);
		serifA.getTestAt(8).setAnswerAt(3, 0.0);
		
		serifA.getTestAt(9).setAnswerAt(0, 1.0);
		serifA.getTestAt(9).setAnswerAt(1, 0.0);
		serifA.getTestAt(9).setAnswerAt(2, 0.0);
		serifA.getTestAt(9).setAnswerAt(3, 1.0);
		//
		
		
		serifB.getTestAt(0).setAnswerAt(0, 1.0);
		serifB.getTestAt(0).setAnswerAt(1, 1.0);
		serifB.getTestAt(0).setAnswerAt(2, 1.0);
		serifB.getTestAt(0).setAnswerAt(3, 1.0);
		
		serifB.getTestAt(1).setAnswerAt(0, 1.0);
		serifB.getTestAt(1).setAnswerAt(1, 0.0);
		serifB.getTestAt(1).setAnswerAt(2, 1.0);
		serifB.getTestAt(1).setAnswerAt(3, 0.0);
		
		serifB.getTestAt(2).setAnswerAt(0, 0.0);
		serifB.getTestAt(2).setAnswerAt(1, 0.0);
		serifB.getTestAt(2).setAnswerAt(2, 1.0);
		serifB.getTestAt(2).setAnswerAt(3, 0.0);
		
		serifB.getTestAt(3).setAnswerAt(0, 1.0);
		serifB.getTestAt(3).setAnswerAt(1, 1.0);
		serifB.getTestAt(3).setAnswerAt(2, 1.0);
		serifB.getTestAt(3).setAnswerAt(3, 1.0);
		
		serifB.getTestAt(4).setAnswerAt(0, 1.0);
		serifB.getTestAt(4).setAnswerAt(1, 1.0);
		serifB.getTestAt(4).setAnswerAt(2, 0.0);
		serifB.getTestAt(4).setAnswerAt(3, 1.0);
		
		serifB.getTestAt(5).setAnswerAt(0, 1.0);
		serifB.getTestAt(5).setAnswerAt(1, 0.0);
		serifB.getTestAt(5).setAnswerAt(2, 0.0);
		serifB.getTestAt(5).setAnswerAt(3, 0.0);
	
		serifB.getTestAt(6).setAnswerAt(0, 1.0);
		serifB.getTestAt(6).setAnswerAt(1, 0.0);
		serifB.getTestAt(6).setAnswerAt(2, 1.0);
		serifB.getTestAt(6).setAnswerAt(3, 1.0);
	
		serifB.getTestAt(7).setAnswerAt(0, 1.0);
		serifB.getTestAt(7).setAnswerAt(1, 0.0);
		serifB.getTestAt(7).setAnswerAt(2, 0.0);
		serifB.getTestAt(7).setAnswerAt(3, 1.0);
		
		serifB.getTestAt(8).setAnswerAt(0, 0.0);
		serifB.getTestAt(8).setAnswerAt(1, 0.0);
		serifB.getTestAt(8).setAnswerAt(2, 0.0);
		serifB.getTestAt(8).setAnswerAt(3, 1.0);
		
		serifB.getTestAt(9).setAnswerAt(0, 0.0);
		serifB.getTestAt(9).setAnswerAt(1, 1.0);
		serifB.getTestAt(9).setAnswerAt(2, 0.0);
		serifB.getTestAt(9).setAnswerAt(3, 0.0);
		
		//
		sansA.getTestAt(0).setAnswerAt(0, 1.0);
		sansA.getTestAt(0).setAnswerAt(1, 0.0);
		sansA.getTestAt(0).setAnswerAt(2, 0.0);
		sansA.getTestAt(0).setAnswerAt(3, 1.0);
		
		sansA.getTestAt(1).setAnswerAt(0, 1.0);
		sansA.getTestAt(1).setAnswerAt(1, 1.0);
		sansA.getTestAt(1).setAnswerAt(2, 1.0);
		sansA.getTestAt(1).setAnswerAt(3, 0.0);
		
		sansA.getTestAt(2).setAnswerAt(0, 0.0);
		sansA.getTestAt(2).setAnswerAt(1, 1.0);
		sansA.getTestAt(2).setAnswerAt(2, 0.0);
		sansA.getTestAt(2).setAnswerAt(3, 0.0);
		
		sansA.getTestAt(3).setAnswerAt(0, 1.0);
		sansA.getTestAt(3).setAnswerAt(1, 1.0);
		sansA.getTestAt(3).setAnswerAt(2, 1.0);
		sansA.getTestAt(3).setAnswerAt(3, 1.0);
		
		sansA.getTestAt(4).setAnswerAt(0, 1.0);
		sansA.getTestAt(4).setAnswerAt(1, 1.0);
		sansA.getTestAt(4).setAnswerAt(2, 0.0);
		sansA.getTestAt(4).setAnswerAt(3, 1.0);
		
		sansA.getTestAt(5).setAnswerAt(0, 1.0);
		sansA.getTestAt(5).setAnswerAt(1, 0.0);
		sansA.getTestAt(5).setAnswerAt(2, 0.0);
		sansA.getTestAt(5).setAnswerAt(3, 0.0);
	
		sansA.getTestAt(6).setAnswerAt(0, 1.0);
		sansA.getTestAt(6).setAnswerAt(1, 1.0);
		sansA.getTestAt(6).setAnswerAt(2, 0.0);
		sansA.getTestAt(6).setAnswerAt(3, 1.0);
	
		sansA.getTestAt(7).setAnswerAt(0, 1.0);
		sansA.getTestAt(7).setAnswerAt(1, 0.0);
		sansA.getTestAt(7).setAnswerAt(2, 0.0);
		sansA.getTestAt(7).setAnswerAt(3, 1.0);
		
		sansA.getTestAt(8).setAnswerAt(0, 1.0);
		sansA.getTestAt(8).setAnswerAt(1, 1.0);
		sansA.getTestAt(8).setAnswerAt(2, 0.0);
		sansA.getTestAt(8).setAnswerAt(3, 1.0);
		
		sansA.getTestAt(9).setAnswerAt(0, 0.0);
		sansA.getTestAt(9).setAnswerAt(1, 0.0);
		sansA.getTestAt(9).setAnswerAt(2, 1.0);
		sansA.getTestAt(9).setAnswerAt(3, 1.0);
		
		//
		
		sansB.getTestAt(0).setAnswerAt(0, 1.0);
		sansB.getTestAt(0).setAnswerAt(1, 0.0);
		sansB.getTestAt(0).setAnswerAt(2, 0.0);
		sansB.getTestAt(0).setAnswerAt(3, 0.0);
		
		sansB.getTestAt(1).setAnswerAt(0, 1.0);
		sansB.getTestAt(1).setAnswerAt(1, 0.0);
		sansB.getTestAt(1).setAnswerAt(2, 0.0);
		sansB.getTestAt(1).setAnswerAt(3, 0.0);
		
		sansB.getTestAt(2).setAnswerAt(0, 1.0);
		sansB.getTestAt(2).setAnswerAt(1, 0.0);
		sansB.getTestAt(2).setAnswerAt(2, 1.0);
		sansB.getTestAt(2).setAnswerAt(3, 0.0);
		
		sansB.getTestAt(3).setAnswerAt(0, 1.0);
		sansB.getTestAt(3).setAnswerAt(1, 0.0);
		sansB.getTestAt(3).setAnswerAt(2, 1.0);
		sansB.getTestAt(3).setAnswerAt(3, 1.0);
		
		sansB.getTestAt(4).setAnswerAt(0, 1.0);
		sansB.getTestAt(4).setAnswerAt(1, 1.0);
		sansB.getTestAt(4).setAnswerAt(2, 0.0);
		sansB.getTestAt(4).setAnswerAt(3, 1.0);
	
		sansB.getTestAt(5).setAnswerAt(0, 1.0);
		sansB.getTestAt(5).setAnswerAt(1, 0.0);
		sansB.getTestAt(5).setAnswerAt(2, 0.0);
		sansB.getTestAt(5).setAnswerAt(3, 1.0);
	
		sansB.getTestAt(6).setAnswerAt(0, 1.0);
		sansB.getTestAt(6).setAnswerAt(1, 0.0);
		sansB.getTestAt(6).setAnswerAt(2, 0.0);
		sansB.getTestAt(6).setAnswerAt(3, 1.0);
	
		sansB.getTestAt(7).setAnswerAt(0, 1.0);
		sansB.getTestAt(7).setAnswerAt(1, 0.0);
		sansB.getTestAt(7).setAnswerAt(2, 0.0);
		sansB.getTestAt(7).setAnswerAt(3, 1.0);
		
		sansB.getTestAt(8).setAnswerAt(0, 1.0);
		sansB.getTestAt(8).setAnswerAt(1, 0.0);
		sansB.getTestAt(8).setAnswerAt(2, 1.0);
		sansB.getTestAt(8).setAnswerAt(3, 0.0);
		
		sansB.getTestAt(9).setAnswerAt(0, 1.0);
		sansB.getTestAt(9).setAnswerAt(1, 0.0);
		sansB.getTestAt(9).setAnswerAt(2, 0.0);
		sansB.getTestAt(9).setAnswerAt(3, 0.0);

		
		System.out.println("\ntotal average (laughter epidemic - serif): " + serifA.totalAverage());
		for(int i = 0; i < 4; i++){
			
			System.out.println("average for question " + (i+1) + " (laughter epidemic - serif): " + serifA.questionAverage(i));
		}
		System.out.println("\ntotal average (laughter epidemic - sans): " + sansB.totalAverage());
		for(int i = 0; i < 4; i++){
			
			System.out.println("average for question " + (i+1) + " (laughter epidemic - sans): " + sansB.questionAverage(i));
		}	
		System.out.println("\ntotal average (wafflehouse - serif): " + serifB.totalAverage());
		for(int i = 0; i < 4; i++){
			
			System.out.println("average for question " + (i+1) + " (wafflehouse - serif): " + serifB.questionAverage(i));
		}
		System.out.println("\ntotal average wafflehouse - sans): " + sansA.totalAverage());
		for(int i = 0; i < 4; i++){
			
			System.out.println("average for question " + (i+1) + " (wafflehouse - sans): " + sansA.questionAverage(i));
		}
		
		
		for(int i = 0; i < 10; i++)
		{
			Test SEA = serifA.getTestAt(i);
			Test SA = sansA.getTestAt(i);
			Test SEB = serifB.getTestAt(i);
			Test SB = sansB.getTestAt(i);
			System.out.println(SEA.getAverage() + " " + SA.getAverage() + " " + SEB.getAverage() + " " + SB.getAverage());	
		}
		
		
	}

}
