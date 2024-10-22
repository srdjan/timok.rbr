DELETE FROM [RbrDb_267].[dbo].[TerminationChoice]
	where [RbrDb_267].[dbo].[TerminationChoice].[routing_plan_id] in (
										SELECT [RbrDb_267].[dbo].[RoutingPlan].[routing_plan_id] FROM [RbrDb_267].[dbo].[RoutingPlan]
										where name like 'z%'
									)


DELETE FROM [RbrDb_267].[dbo].[RoutingPlanDetail]
 where [RbrDb_267].[dbo].[RoutingPlanDetail].[routing_plan_id] in (
										SELECT [RbrDb_267].[dbo].[RoutingPlan].[routing_plan_id] FROM [RbrDb_267].[dbo].[RoutingPlan]
										where name like 'z%'
									)
UPDATE [RbrDb_267].[dbo].[Service]
   SET [default_routing_plan_id] = 1
   WHERE [default_routing_plan_id] in (
										SELECT [RbrDb_267].[dbo].[RoutingPlan].[routing_plan_id] FROM [RbrDb_267].[dbo].[RoutingPlan]
										where name like 'z%'
									   )

select * FROM [RbrDb_267].[dbo].[RoutingPlan]
 where [RbrDb_267].[dbo].[RoutingPlan].[routing_plan_id] in (
										SELECT [RbrDb_267].[dbo].[RoutingPlan].[routing_plan_id] FROM [RbrDb_267].[dbo].[RoutingPlan]
										where name like 'z%'
									)