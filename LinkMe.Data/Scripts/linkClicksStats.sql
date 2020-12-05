drop table #tempClicks

Select Count(lc.Id) as s, lc.CountryCode
into #tempClicks
from LinkClicks as lc
where lc.LinkId = 4 
group by lc.CountryCode

go

select c.CountryCode, t.s from Countries as c left join #tempClicks as t on c.CountryCode = t.CountryCode