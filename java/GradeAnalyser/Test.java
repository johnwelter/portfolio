package GradeAnalyser;

import javax.swing.Spring;

public class Test {
	
	private String name;
	private double[] results;
	private double totalScore;
	
	public Test(int answers, double total, String taker )
	{
		
		results = new double[answers];
		totalScore = total;
		name = taker;
		
	}
	
	public int size()
	{
		return results.length;
		
	}
	
	public void setAnswerAt(int index, double score)
	{
		
		results[index] = score;
		
	}
	
	public double getAnswerAt(int index)
	{
		
		return results[index];
		
	}
	
	public double getAverage()
	{
		double score = 0.0;
		for(int i = 0; i < results.length; i++)
		{
			
			score += results[i];
			
		}
		
		return 100.0*(score/totalScore);
		
	}
	
	public String getTaker()
	{
		return name;
	}
	

}
