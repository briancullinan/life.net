-- address with emails in display names replaced with the proper format
/*
update email set [from] = replacement from email inner join (select *,Replace(REPLACE([from], concat('" <', [value], '>"'), '"'), '""', '"') as replacement from (
select *,substring([from], start + 1, patindex('%>%', [from]) - start - 1) As value from (
Select Id,convert(varchar(1000), [From]) as [from],[Subject], PATINDEX('%<%', [from]) as start from email where [From] like '%<%>%'
) as startindex where [from] like '%""%'
and [from] not like '%"<%'
and [from] not like '%>"'
) as extractedemail) as replacements on email.Id = replacements.id
*/
-- fill empty addresses because it wasn't detected during insert
/*
update email set [from] = replacement from email inner join (
select *,rtrim(ltrim(substring(headerssub, 0, [stop]))) as [replacement] from (
select *,patindex('%'+char(13)+'%', headerssub) as [stop] from (
select *,substring([headers], start + 7, len([headers]) - start - 7) as headerssub from (
Select Id,headers,convert(varchar(1000), [From]) as [from],[Subject], PATINDEX('%'+char(13)+char(10)+'From:%', [headers]) as start from email where [From] not like '%<%>%'
) as patstart
) as headerssub
) as patstop
) as withreplacement
on withreplacement.id = email.id;
*/
-- insert missing relationships
/*
insert into RelationText ([Left], [Right], [Type])
select [from],display,'Email.Activities.CheckMail' as [type] from (
select *,rtrim(ltrim(replace(substring([from], 0, start), '"', ''))) as display from (
select *,substring([from], start + 1, patindex('%>%', [from]) - start - 1) As value from (
Select Id,convert(varchar(1000), [From]) as [from],[Subject], PATINDEX('%<%', [from]) as start from email where [From] like '%<%>%'
) as startindex
) as extractedemail
) as withdisplay where display != ''
*/