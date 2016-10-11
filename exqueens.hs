type Queen = (Int,Int)
type Setup = [Queen]

threatens :: Queen -> Queen -> Bool
threatens (a1,a2) (b1,b2)
   | (a1,a2) == (b1,b2) = False -- Queen doesn't threaten herself
   | a2 == b2           = True  -- On the same row
   | a1 == b1           = True  -- On the same column
   | (a1-a2) == (b1-b2) = True  -- diagonal
   | (a1+a2) == (b1+b2) = True  -- diagonal
   | otherwise = False

queens :: (Int,Int) -> [Setup]
queens (s,e) = if(s == e) then addQueen s [] else combiner (queens (s,h)) (queens (h+1,e))
  where h = s + (e-s) `div` 2

addQueen :: Int -> Setup -> [Setup]
addQueen x qs = [new:qs | y <- [1 .. 8], let new = (x,y)]

combiner :: [Setup] -> [Setup] -> [Setup]
combiner xs ys = [x++y | x <- xs, y <- ys, not (threatened (x++y))]

threatened :: Setup -> Bool
threatened [] = False
threatened (x:xs) = if(any (threatens x) xs) then True else threatened xs